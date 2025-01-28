using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerServerAPIHandler : MonoBehaviour, ISaveLoad
{
    public static event Action<Player> OnPlayerDataLoaded;
    public static event Action<Player> OnPlayerDataSaved;

    [SerializeField] private Player _playerData;
    [SerializeField] private DataType _dataType;

    public DataType GetDataType() => _dataType;
    public Player PlayerDataPresent => _playerData;

    private async void Awake() => await LoadAsync();

    [ContextMenu("LoadAsync")]
    public async Task LoadAsync()
    {
        using (HttpClient httpClient = new HttpClient()) 
        {
            string URL = APIContainer.API_PLAYER_GET_BY_ID;
            Debug.Log($"[PlayerServerAPIHandler] Sending GET request to {URL}");

            HttpResponseMessage response = await httpClient.GetAsync(URL);
            if (response.IsSuccessStatusCode) 
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                _playerData = JsonUtility.FromJson<Player>(jsonData);
                
                Debug.Log("[PlayerServerAPIHandler] LoadAsync |SUCCESS| Data Loaded from Server");
                OnPlayerDataLoaded?.Invoke(_playerData);
            }
            else
            {
                Debug.LogWarning($"[PlayerServerAPIHandler] LoadAsync |FAILED| Status: {response.StatusCode}");
                CreateDefaultPlayerData();
                OnPlayerDataLoaded?.Invoke(_playerData);
            }
        }
    }

    [ContextMenu("Save")]
    public async void Save()
    {
        using (HttpClient httpClient = new HttpClient()) 
        {
            string URL = APIContainer.API_PLAYER_SET_BY_ID;
            string jsonData = JsonUtility.ToJson(_playerData);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            Debug.Log($"[PlayerServerAPIHandler] Sending POST request to {URL}");
            HttpResponseMessage response = await httpClient.PostAsync(URL, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.Log("[PlayerServerAPIHandler] Save |SUCCESS| Data Saved to Server");
                OnPlayerDataSaved?.Invoke(_playerData);
            }
            else
            {
                Debug.LogWarning($"[PlayerServerAPIHandler] Save |FAILED| Status: {response.StatusCode}");
            }
        }
    }

    private void CreateDefaultPlayerData()
    {
        Debug.Log("[PlayerServerAPIHandler] Created Default Player Data");
        _playerData = new Player
        {
            ID = "1",
            Username = "Nguyen Thai Bao",
            Email = "baonguyen.devg@gmail.com",
            Score = 0,
            CurrentLocation = Vector3.zero,
        };
    }
}
