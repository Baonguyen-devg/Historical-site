using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveLoadLocalStorage
{
    [SerializeField] protected static bool _isEncryptData = false;
    protected static string _fileExtension = ".json";

    public static async void SaveDataAsync<T>(T data, string savePath) 
    {
        if (!ValidateData(data))
        {
            Debug.LogWarning("[GenericSaveLoadData] SaveDataAsync |FAIL| Data validation failed!");
            return;
        }
        
        EnsureJsonExtension(savePath);
        Debug.Log($"[GenericSaveLoadData] SaveDataAsync: {typeof(T)} into local storage, path: {savePath}");

        string json = JsonUtility.ToJson(data, true);
        if (_isEncryptData) json = EncryptData(json);
        try 
        {
            await File.WriteAllTextAsync(savePath, json);
        }
        catch (Exception exception) 
        {
            Debug.LogError($"[GenericSaveLoadData] SaveDataAsync |FAIL| {exception.Message}");
        }
    }

    public static async Task<T> LoadDataAsync<T>(string savePath) 
    {
        EnsureJsonExtension(savePath);
        if (File.Exists(savePath)) 
        {
            try
            {
                string json = await File.ReadAllTextAsync(savePath);
                if (_isEncryptData) json = DecryptData(json);
                Debug.Log($"[GenericSaveLoadData] LoadDataAsync: {typeof(T)} from local storage, path: {savePath}");
                T data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[GenericSaveLoadData] LoadDataAsync |FAIL| {exception.Message}");
                return default(T);
            }
        } 
        else 
        {
            Debug.LogWarning("[GenericSaveLoadData] LoadDataAsync |FAIL| Save file not found!");
            return default(T);
        }
    }

    public static void DeleteData(string savePath) 
    {
        EnsureJsonExtension(savePath);
        if (File.Exists(savePath)) 
        {
            File.Delete(savePath);
            Debug.Log($"[GenericSaveLoadData] DeleteData: {savePath} has been deleted.");
        } 
        else 
        {
            Debug.LogWarning("[GenericSaveLoadData] DeleteData |FAIL| Save file not found!");
        }
    }

    protected static string EnsureJsonExtension(string path)
    {
        if (!Path.GetExtension(path).Equals(_fileExtension, StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogWarning($"[GenericSaveLoadData] EnsureJsonExtension | Path: {path}");
            path += _fileExtension;
        }
        return path;
    }

    protected static string EncryptData(string data) 
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));
    }

    protected static string DecryptData(string data)
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(data));
    }

    protected static bool ValidateData<T>(T data) 
    {
        return true;
    }
}
