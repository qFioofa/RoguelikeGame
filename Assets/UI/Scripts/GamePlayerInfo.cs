using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamePlayerInfo : MonoBehaviour
{
    public PlayerInfo Info;
    public WeaponHandler GUNS;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI MONEY;
    public TextMeshProUGUI AMMO;
    
    void Start()
    {
        Info = GameObject.FindWithTag("Player")?.GetComponent<PlayerInfo>();
        GUNS = GameObject.FindWithTag("Player")?.GetComponent<WeaponHandler>();
    }

    void Update()
    {
        HP.text = "b " + Info.Health.ToString();
        MONEY.text = "$ " + Info.Money.ToString();
        //AMMO.text = GUNS.GetInfoWeapon().WeaponType;
        //AMMO.text = $"{GUNS.GetInfoWeapon().currentAmmo} | {GUNS.GetInfoWeapon().backPackAmmo}";
        switch (GUNS.GetInfoWeapon().WeaponType.ToString()) {
            case "Galil":
                {
                    AMMO.text = "B " + $"{GUNS.GetInfoWeapon().currentAmmo} | {GUNS.GetInfoWeapon().backPackAmmo}";
                    break;
                }
            
            case "p228":
                {
                    AMMO.text = "A " + $"{GUNS.GetInfoWeapon().currentAmmo} | {GUNS.GetInfoWeapon().backPackAmmo}";
                    break;
                }
            case "":
                {
                    AMMO.text = "H " + $"{GUNS.GetInfoWeapon().currentAmmo} | {GUNS.GetInfoWeapon().backPackAmmo}";
                    break;
                }
            case "Knife":
                {
                    AMMO.text = "J";
                    break;
                }
        }
    }
}