using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private float HealthLimit = 100f;
    [SerializeField] private float _Health = 75f;
    [SerializeField] private int _Money = 0;
    private WeaponHandler weaponHandler;

    void Start(){
        weaponHandler = GetComponent<WeaponHandler>();
    }
    public int Money{
        get { return _Money; }
    }

    public void AddOneMag(){
        weaponHandler.AddOneMag();
    }
    public void AddMoney(int money) => _Money+=money;
    public void SpendMoney(int money) => _Money=_Money-money > 0 ? _Money-money : 0; 
    public void AddHealth(int health) => _Health=health +_Health >= HealthLimit ? HealthLimit : health +_Health;
    public void Damage(int health) => _Health-=health;
    public bool IsDead() => _Health <=0;
}
