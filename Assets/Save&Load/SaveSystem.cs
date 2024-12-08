using System;
using System.IO;
using UnityEngine;

public class SaveSystem : JsonService {
    public string fileName = "rog_save";
    public readonly string fileExtension = ".json";
    public void SaveData<T>(string relativePath, T data) {
        string directoryPath = Path.Combine(Application.persistentDataPath, relativePath);
        if (!Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }

        string path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log($"Saved to {path}: {json}");
    }   

    public T LoadData<T>(string relativePath) {
        string path = Path.Combine(Application.persistentDataPath, relativePath, $"{fileName}{fileExtension}");
        if (!File.Exists(path)) {
            Debug.LogWarning($"File not found at {path}");
            throw new FileNotFoundException($"File not found at {path}");
        }

        string json = File.ReadAllText(path);
        T result = JsonUtility.FromJson<T>(json);
        Debug.Log($"Loaded from {path}: {json}");
        return result;
    }
}
