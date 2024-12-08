using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Info", menuName = "Player/Player_Info")]
public class PlayerInfo : MonoBehaviour {
    [SerializeField] private PlayerData playerData;
    private WeaponHandler weaponHandler;

    void Start(){
        weaponHandler = GetComponent<WeaponHandler>();
    }
    public int Money{
        get { return playerData.Money; }
    }

    public float Health{
        get { return playerData.Health; }
    }

    public void AddOneMag(){
        weaponHandler.AddOneMag();
    }
    public void AddMoney(int money) => playerData.Money+=money;
    public void SpendMoney(int money) => playerData.Money=playerData.Money-money > 0 ? playerData.Money-money : 0; 
    public void AddHealth(int health) => playerData.Health=health +playerData.Health >= playerData.HealthLimit ? playerData.HealthLimit : health +playerData.Health;
    public void Damage(int health) => playerData.Health-=health;
    public bool IsDead() => playerData.Health <=0;
}
