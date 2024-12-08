using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour {
    public static SaveData saveData;
    private static SaveSystem saveSystem =  new SaveSystem();
    void Start(){
        Load();
    }

    public static void SaveLoad(){
        Save();
        Load();
    }

    public static void Load(){
        try{
            saveData = SaveData.FromAttributes(saveSystem.LoadData<SaveDataAttributes>("Save"));
            CommitChanges();
        }catch(Exception){
            InitFile();
        }
    }
    public static  void Save(){
        saveSystem.SaveData("Save", SaveData.FromScriptableObject(saveData));
    }

    public static void ResetData(){
        PlayerData.Copy(saveData.playerData, saveData.playerDataReset);
        WeaponData.Copy(saveData.galilData, saveData.galilWeaponDataReset);
        WeaponData.Copy(saveData.p228Data, saveData.p228WeaponDataReset);
        ShopData.Copy(saveData.shopData, saveData.shopDataReset);
        Save();
    }
    void OnApplicationQuit(){
        Save();
    }
    private static void CommitChanges(){

    }

    private static void InitFile(){

    }
}
