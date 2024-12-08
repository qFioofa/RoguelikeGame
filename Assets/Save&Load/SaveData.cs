using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Save Data")]
public class SaveData : ScriptableObject {
    [Header("Player")]
    public PlayerData playerData;
    [Header("Weapon")]
    public WeaponData knifeData;
    public WeaponData p228Data;
    public WeaponData galilData;
    public WeaponData granadeData;
    [Header("Shop")]
    public ShopData shopData;

    [Header("Reset")]
    public PlayerData playerDataReset;
    public WeaponData knifeWeaponDataReset;
    public WeaponData p228WeaponDataReset;
    public WeaponData galilWeaponDataReset;
    public WeaponData granadeWeaponDataReset;
    public ShopData shopDataReset;

    public static SaveDataAttributes FromScriptableObject(SaveData saveData) {
        return new SaveDataAttributes {
            playerData = PlayerDataAttributes.FromScriptableObject(saveData.playerData),
            knifeData = WeaponDataAttributes.FromScriptableObject(saveData.knifeData),
            p228Data = WeaponDataAttributes.FromScriptableObject(saveData.p228Data),
            galilData = WeaponDataAttributes.FromScriptableObject(saveData.galilData),
            granadeData = WeaponDataAttributes.FromScriptableObject(saveData.granadeData),
            shopData = ShopDataAttributes.FromScriptableObject(saveData.shopData),
            playerDataReset = PlayerDataAttributes.FromScriptableObject(saveData.playerDataReset),
            knifeWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.knifeWeaponDataReset),
            p228WeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.p228WeaponDataReset),
            galilWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.galilWeaponDataReset),
            granadeWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.granadeData),
            shopDataReset = ShopDataAttributes.FromScriptableObject(saveData.shopDataReset)
        };
    }

    public static SaveData FromAttributes(SaveDataAttributes saveDataAttributes) {
        SaveData saveData = ScriptableObject.CreateInstance<SaveData>();
        
        saveData.playerData = PlayerData.FromAttributes(saveDataAttributes.playerData);
        saveData.knifeData = WeaponData.FromAttributes(saveDataAttributes.knifeData);
        saveData.p228Data = WeaponData.FromAttributes(saveDataAttributes.p228Data);
        saveData.galilData = WeaponData.FromAttributes(saveDataAttributes.galilData);
        saveData.granadeData = WeaponData.FromAttributes(saveDataAttributes.granadeData);
        saveData.shopData = ShopData.FromAttributes(saveDataAttributes.shopData);
        
        saveData.playerDataReset = PlayerData.FromAttributes(saveDataAttributes.playerDataReset);
        saveData.knifeWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.knifeWeaponDataReset);
        saveData.p228WeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.p228WeaponDataReset);
        saveData.galilWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.galilWeaponDataReset);
        saveData.granadeWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.granadeWeaponDataReset);
        saveData.shopDataReset = ShopData.FromAttributes(saveDataAttributes.shopDataReset);

        return saveData;
    }

    public void Reset() {
        PlayerData.Copy(playerData, playerDataReset);
        WeaponData.Copy(knifeData, knifeWeaponDataReset);
        WeaponData.Copy(p228Data, p228WeaponDataReset);
        WeaponData.Copy(galilData, galilWeaponDataReset);
        WeaponData.Copy(granadeData, granadeWeaponDataReset);
        ShopData.Copy(shopData, shopDataReset);
    }
}

[System.Serializable]
public class SaveDataAttributes {
    [Header("Player")]
    public PlayerDataAttributes playerData;
    [Header("Weapon")]
    public WeaponDataAttributes knifeData;
    public WeaponDataAttributes p228Data;
    public WeaponDataAttributes galilData;
    public WeaponDataAttributes granadeData;
    [Header("Shop")]
    public ShopDataAttributes shopData;

    [Header("Reset")]
    public PlayerDataAttributes playerDataReset;
    public WeaponDataAttributes knifeWeaponDataReset;
    public WeaponDataAttributes p228WeaponDataReset;
    public WeaponDataAttributes galilWeaponDataReset;
    public WeaponDataAttributes granadeWeaponDataReset;
    public ShopDataAttributes shopDataReset;

    public static SaveDataAttributes FromScriptableObject(SaveData saveData) {
        return new SaveDataAttributes {
            playerData = PlayerDataAttributes.FromScriptableObject(saveData.playerData),
            knifeData = WeaponDataAttributes.FromScriptableObject(saveData.knifeData),
            p228Data = WeaponDataAttributes.FromScriptableObject(saveData.p228Data),
            galilData = WeaponDataAttributes.FromScriptableObject(saveData.galilData),
            granadeData = WeaponDataAttributes.FromScriptableObject(saveData.granadeData),
            shopData = ShopDataAttributes.FromScriptableObject(saveData.shopData),
            playerDataReset = PlayerDataAttributes.FromScriptableObject(saveData.playerDataReset),
            knifeWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.knifeWeaponDataReset),
            p228WeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.p228WeaponDataReset),
            galilWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.galilWeaponDataReset),
            granadeWeaponDataReset = WeaponDataAttributes.FromScriptableObject(saveData.granadeData),
            shopDataReset = ShopDataAttributes.FromScriptableObject(saveData.shopDataReset)
        };
    }

    public static SaveData FromAttributes(SaveDataAttributes saveDataAttributes) {
        SaveData saveData = ScriptableObject.CreateInstance<SaveData>();
        
        saveData.playerData = PlayerData.FromAttributes(saveDataAttributes.playerData);
        saveData.knifeData = WeaponData.FromAttributes(saveDataAttributes.knifeData);
        saveData.p228Data = WeaponData.FromAttributes(saveDataAttributes.p228Data);
        saveData.galilData = WeaponData.FromAttributes(saveDataAttributes.galilData);
        saveData.granadeData = WeaponData.FromAttributes(saveDataAttributes.granadeData);
        saveData.shopData = ShopData.FromAttributes(saveDataAttributes.shopData);
        
        saveData.playerDataReset = PlayerData.FromAttributes(saveDataAttributes.playerDataReset);
        saveData.knifeWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.knifeWeaponDataReset);
        saveData.p228WeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.p228WeaponDataReset);
        saveData.galilWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.galilWeaponDataReset);
        saveData.granadeWeaponDataReset = WeaponData.FromAttributes(saveDataAttributes.granadeWeaponDataReset);
        saveData.shopDataReset = ShopData.FromAttributes(saveDataAttributes.shopDataReset);

        return saveData;
    }

    public void Reset() {
        PlayerDataAttributes.Copy(playerData, playerDataReset);
        WeaponDataAttributes.Copy(knifeData, knifeWeaponDataReset);
        WeaponDataAttributes.Copy(p228Data, p228WeaponDataReset);
        WeaponDataAttributes.Copy(galilData,galilWeaponDataReset);
        WeaponDataAttributes.Copy(granadeData, granadeWeaponDataReset);
        ShopDataAttributes.Copy(shopData, shopDataReset);
    }
}