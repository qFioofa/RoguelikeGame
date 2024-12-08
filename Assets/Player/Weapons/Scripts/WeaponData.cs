using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject {
    [Header("Info")]
    public string WeaponType;
    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int backPackAmmo;
    public int magCapacity;
    public float fireRate;

    [Header("Sounds")]
    public AudioClip MagEmpty;
    public AudioClip Draw;
    public AudioClip[] Reload;
    public AudioClip Shoot;
    public static void Copy(WeaponData from,WeaponData other){
        from.WeaponType = other.WeaponType;
        from.damage = other.damage;
        from.maxDistance = other.maxDistance;
        from.currentAmmo = other.currentAmmo;
        from.backPackAmmo = other.backPackAmmo;
        from.magCapacity = other.magCapacity;
        from.fireRate = other.fireRate;
        from.MagEmpty = other.MagEmpty;
        from.Draw = other.Draw;
        from.Reload = other.Reload;
        from.Shoot = other.Shoot;
    }
}