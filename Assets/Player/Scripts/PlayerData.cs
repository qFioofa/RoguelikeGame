using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Player Info")]
public class PlayerData : ScriptableObject {
    public float HealthLimit = 100f;
    public float Health = 75f;
    public int Money = 0;
    public float StartHp = 0.75f;

    public void InitStartValues(){
        Health = (int)(HealthLimit*StartHp);
    }

    public static PlayerDataAttributes FromScriptableObject(PlayerData saveData) {
        return new PlayerDataAttributes {
            HealthLimit = saveData.HealthLimit,
            Health = saveData.Health,
            Money = saveData.Money
        };
    }

    public static PlayerData FromAttributes(PlayerDataAttributes saveData) {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
        playerData.HealthLimit = saveData.HealthLimit;
        playerData.Health = saveData.Health;
        playerData.Money = saveData.Money;
        return playerData;
    }

    public static void Copy(PlayerData from, PlayerData other){
        from.HealthLimit = other.HealthLimit;
        from.Health = other.Health;
        from.Money = other.Money;
    }
}

[System.Serializable]
public class PlayerDataAttributes {
    public float HealthLimit = 100f;
    public float Health = 75f;
    public int Money = 0;
    public static PlayerDataAttributes FromScriptableObject(PlayerData saveData) {
        return new PlayerDataAttributes {
            HealthLimit = saveData.HealthLimit,
            Health = saveData.Health,
            Money = saveData.Money
        };
    }

    public static PlayerData FromAttributes(PlayerDataAttributes saveData) {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
        playerData.HealthLimit = saveData.HealthLimit;
        playerData.Health = saveData.Health;
        playerData.Money = saveData.Money;
        return playerData;
    }

    public static void Copy(PlayerDataAttributes from, PlayerDataAttributes other) {
        from.HealthLimit = other.HealthLimit;
        from.Health = other.Health;
        from.Money = other.Money;
    }
}
