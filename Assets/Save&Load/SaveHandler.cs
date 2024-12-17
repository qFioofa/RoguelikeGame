using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour {
    public static string Directory = "Save";
    [SerializeField] public SaveData saveData;
    private static SaveSystem saveSystem =  new SaveSystem();

    private static bool loaded;
    void Start(){
        Load();
    }

    public void Load(){
        if(loaded) return;

        try{
            SaveData fileData = SaveData.FromAttributes(saveSystem.LoadData<SaveDataAttributes>(Directory));
            CommitChanges(fileData);
        }catch(Exception){
            InitFile(Directory);
        }

        loaded = true;
    }
    public void Save() => saveSystem.SaveData(Directory, SaveDataAttributes.FromScriptableObject(saveData));
    void OnApplicationQuit() => Save();
    private void CommitChanges(SaveData fileData) => SaveData.Copy(fileData, saveData);
    private void InitFile(string dir) => saveSystem.CreateDirectory(dir);
}
