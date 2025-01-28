using System;
using System.Threading.Tasks;
using UnityEngine;

//Local storage - database - only a player saved
public class PlayerLocalStorageHandler : MonoBehaviour, ISaveLoad
{
    private readonly string k_FilePath = "/PlayerData";

    public static event Action<Player> OnPlayerDataLoaded;
    public static event Action<Player> OnPlayerDataSaved;

    [SerializeField] private DataType _dataType;
    [SerializeField] private Player _playerData;

    public DataType GetDataType() => _dataType;
    public Player PlayerDataPresent => _playerData;

    private bool _isInitialized = false;
    public bool IsInitialized => _isInitialized;
    
    public void Awake() => Init();
    public async void Init() 
    {
        await LoadAsync();  
        _isInitialized = true;
        Debug.Log("[PlayerLocalStorageHandler] Init |SUCCESS|");
    }

    [ContextMenu("LoadAsync")] 
    public async Task LoadAsync()
    {
        string path = Application.persistentDataPath + k_FilePath;
        _playerData = await SaveLoadLocalStorage.LoadDataAsync<Player>(path);  
        if (_playerData == null)
        {
            await Task.Delay(100);
            Debug.LogWarning("[PlayerLocalStorageHandler] LoadAsync |NULL| create a new instance");
            //Bao: Don't need get save/load the list of player in local database
            _playerData = new Player
            {
                ID = "1",
                Username = "Nguyen Thai Bao",
                Email = "baonguyen.devg@gmail.com",
                Score = 0,
                CurrentLocation = Vector3.zero,
            };
        }

        Debug.Log($"[PlayerLocalStorageHandler] LoadAsync |SUCCESS| FilePath: {path}");
        OnPlayerDataLoaded?.Invoke(_playerData);
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string path = Application.persistentDataPath + k_FilePath;
        Debug.Log($"[PlayerLocalStorageHandler] Save |SUCCESS| FilePath: {path}");
        
        SaveLoadLocalStorage.SaveDataAsync<Player>(_playerData, path);
        OnPlayerDataSaved?.Invoke(_playerData);
    }

    public void Save(Player playerData) 
    {
        Debug.Log($"[PlayerLocalStorageHandler] Save |SUCCESS| playerData: {playerData}");
        _playerData = playerData;
        Save();
    }
}
