using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Player/Player Info")]
public class PlayerData : ScriptableObject {
    public float HealthLimit = 100f;
    public float Health = 75f;
    public int Money = 0;

    public static void Copy(PlayerData from, PlayerData other){
        from.HealthLimit = other.HealthLimit;
        from.Health = other.HealthLimit;
        from.Money = other.Money;
    }
}

