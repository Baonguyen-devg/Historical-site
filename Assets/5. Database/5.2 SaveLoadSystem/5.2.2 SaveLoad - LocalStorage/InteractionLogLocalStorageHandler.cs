using System;
using System.Threading.Tasks;
using UnityEngine;

//Local storage - database - only a Interaction log saved
public class InteractionLogLocalStorageHandler : MonoBehaviour, ISaveLoad
{
     private readonly string k_FilePath = "/InteractionLog";

    public static event Action<InteractionLog> OnInteractionLogLoaded;
    public static event Action<InteractionLog> OnInteractionLogSaved;

    [SerializeField] private DataType _dataType;
    [SerializeField] private InteractionLog _interactionLogData;

    public DataType GetDataType() => _dataType;
    public InteractionLog InteractionLogData => _interactionLogData;

    private bool _isInitialized = false;
    public bool IsInitialized => _isInitialized;
    
    public void Awake() => Init();
    public async void Init() 
    {
        await LoadAsync();  
        _isInitialized = true;
        Debug.Log("[InteractionLogLocalStorageHandler] Init |SUCCESS|");
    }

    [ContextMenu("LoadAsync")] 
    public async Task LoadAsync()
    {
        string path = Application.persistentDataPath + k_FilePath;
        _interactionLogData = await SaveLoadLocalStorage.LoadDataAsync<InteractionLog>(path);  
        if (_interactionLogData == null)
        {
            await Task.Delay(100);
            Debug.LogWarning("[InteractionLogLocalStorageHandler] LoadAsync |NULL| create a new instance");
            //Bao: Don't need get save/load the list of InteractionLog in local database
            _interactionLogData = new InteractionLog
            {
                ID = "1",
                PlayerID = "1",
                CheckpointID = null,
                ChallengeID = null,
                Timestamp = DateTime.Now,
            };
        }

        Debug.Log($"[InteractionLogLocalStorageHandler] LoadAsync |SUCCESS| FilePath: {path}");
        OnInteractionLogLoaded?.Invoke(_interactionLogData);
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string path = Application.persistentDataPath + k_FilePath;
        Debug.Log($"[InteractionLogLocalStorageHandler] Save |SUCCESS| FilePath: {path}");
        
        SaveLoadLocalStorage.SaveDataAsync<InteractionLog>(_interactionLogData, path);
        OnInteractionLogSaved?.Invoke(_interactionLogData);
    }

    public void Save(InteractionLog interactionLog) 
    {
        Debug.Log($"[InteractionLogLocalStorageHandler] Save |SUCCESS| interactionLog: {interactionLog}");
        _interactionLogData = interactionLog;
        Save();
    }
}
