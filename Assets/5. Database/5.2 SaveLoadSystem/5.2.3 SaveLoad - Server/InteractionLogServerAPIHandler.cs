using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InteractionLogServerAPIHandler : MonoBehaviour, ISaveLoad
{
    public static event Action<InteractionLog> OnInteractionLogLoaded;
    public static event Action<InteractionLog> OnInteractionLogSaved;

    [SerializeField] private InteractionLog _interactionLogData;
    [SerializeField] private DataType _dataType;

    public DataType GetDataType() => _dataType;
    public InteractionLog InteractionLogData => _interactionLogData;

    private async void Awake() => await LoadAsync();

    [ContextMenu("LoadAsync")]
    public async Task LoadAsync()
    {
        //TODO: change load async to load async by ID
        using (HttpClient httpClient = new HttpClient()) 
        {
            string URL = APIContainer.API_INTERACTION_LOG_GET_BY_ID;
            Debug.Log($"[InteractionLogServerAPIHandler] Sending GET request to {URL}");

            HttpResponseMessage response = await httpClient.GetAsync(URL);
            if (response.IsSuccessStatusCode) 
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                _interactionLogData = JsonUtility.FromJson<InteractionLog>(jsonData);
                
                Debug.Log("[InteractionLogServerAPIHandler] LoadAsync |SUCCESS| Data Loaded from Server");
                OnInteractionLogLoaded?.Invoke(_interactionLogData);
            }
            else
            {
                Debug.LogWarning($"[InteractionLogServerAPIHandler] LoadAsync |FAILED| Status: {response.StatusCode}");
                CreateDefaultLogData();
                OnInteractionLogLoaded?.Invoke(_interactionLogData);
            }
        }
    }

    [ContextMenu("Save")]
    public async void Save()
    {
        //TODO: change save async to save async by ID
        using (HttpClient httpClient = new HttpClient()) 
        {
            string URL = APIContainer.API_INTERACTION_LOG_SET_BY_ID;
            string jsonData = JsonUtility.ToJson(_interactionLogData);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            Debug.Log($"[InteractionLogServerAPIHandler] Sending POST request to {URL}");
            HttpResponseMessage response = await httpClient.PostAsync(URL, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.Log("[InteractionLogServerAPIHandler] Save |SUCCESS| Data Saved to Server");
                OnInteractionLogSaved?.Invoke(_interactionLogData);
            }
            else
            {
                Debug.LogWarning($"[InteractionLogServerAPIHandler] Save |FAILED| Status: {response.StatusCode}");
            }
        }
    }

    private void CreateDefaultLogData()
    {
        Debug.Log("[InteractionLogServerAPIHandler] Created Default InteractionLog Data");
        _interactionLogData = new InteractionLog
        {
            ID = "1",
            PlayerID = "1",
            CheckpointID = null,
            ChallengeID = null,
            Timestamp = DateTime.Now,
        };
    }
}
