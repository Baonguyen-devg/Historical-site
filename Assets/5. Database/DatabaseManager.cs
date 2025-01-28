using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DataType 
{
    PlayerData,
    InteractionLogData,
}

public class DatabaseManager : Singleton<DatabaseManager>
{
    [SerializeField] private List<ISaveLoad> _saveLoadHandlers = new List<ISaveLoad>();

    protected override void Awake()
    {
        base.Awake();
        _saveLoadHandlers.AddRange(GetComponentsInChildren<ISaveLoad>());
    }

    public ISaveLoad GetHandler(DataType dataType)
    {
        return _saveLoadHandlers.FirstOrDefault(handler => handler.GetDataType() == dataType);
    }
}
