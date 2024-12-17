using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Weapon Data")]
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
    [Header("Persent")]
    public int StartBackpackAmmo = 2;
    public void InitStartValues(){
        currentAmmo = magCapacity;
        backPackAmmo = StartBackpackAmmo*magCapacity;
    }

    public static WeaponDataAttributes FromScriptableObject(WeaponData saveData) {
        return new WeaponDataAttributes {
            WeaponType = saveData.WeaponType,
            damage = saveData.damage,
            maxDistance = saveData.maxDistance,
            currentAmmo = saveData.currentAmmo,
            backPackAmmo = saveData.backPackAmmo,
            magCapacity = saveData.magCapacity,
            fireRate = saveData.fireRate,
            MagEmpty = saveData.MagEmpty,
            Draw = saveData.Draw,
            Reload = saveData.Reload,
            Shoot = saveData.Shoot
        };
    }
    public static WeaponData FromAttributes(WeaponDataAttributes attributes) {
        WeaponData weaponData = ScriptableObject.CreateInstance<WeaponData>();
        weaponData.WeaponType = attributes.WeaponType;
        weaponData.damage = attributes.damage;
        weaponData.maxDistance = attributes.maxDistance;
        weaponData.currentAmmo = attributes.currentAmmo;
        weaponData.backPackAmmo = attributes.backPackAmmo;
        weaponData.magCapacity = attributes.magCapacity;
        weaponData.fireRate = attributes.fireRate;
        weaponData.MagEmpty = attributes.MagEmpty;
        weaponData.Draw = attributes.Draw;
        weaponData.Reload = attributes.Reload;
        weaponData.Shoot = attributes.Shoot;

        return weaponData;
    }
    public static void Copy(WeaponData from,WeaponData other){
        from.WeaponType = other.WeaponType;
        from.damage = other.damage;
        from.maxDistance = other.maxDistance;
        from.currentAmmo = other.currentAmmo;
        from.backPackAmmo = other.backPackAmmo;
        from.magCapacity = other.magCapacity;
        from.fireRate = other.fireRate;
    }
}


[System.Serializable]
public class WeaponDataAttributes {
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

    public static WeaponDataAttributes FromScriptableObject(WeaponData saveData) {
        return new WeaponDataAttributes {
            WeaponType = saveData.WeaponType,
            damage = saveData.damage,
            maxDistance = saveData.maxDistance,
            currentAmmo = saveData.currentAmmo,
            backPackAmmo = saveData.backPackAmmo,
            magCapacity = saveData.magCapacity,
            fireRate = saveData.fireRate,
            MagEmpty = saveData.MagEmpty,
            Draw = saveData.Draw,
            Reload = saveData.Reload,
            Shoot = saveData.Shoot
        };
    }
    public static WeaponData FromAttributes(WeaponDataAttributes attributes) {
        WeaponData weaponData = ScriptableObject.CreateInstance<WeaponData>();
        weaponData.WeaponType = attributes.WeaponType;
        weaponData.damage = attributes.damage;
        weaponData.maxDistance = attributes.maxDistance;
        weaponData.currentAmmo = attributes.currentAmmo;
        weaponData.backPackAmmo = attributes.backPackAmmo;
        weaponData.magCapacity = attributes.magCapacity;
        weaponData.fireRate = attributes.fireRate;
        weaponData.MagEmpty = attributes.MagEmpty;
        weaponData.Draw = attributes.Draw;
        weaponData.Reload = attributes.Reload;
        weaponData.Shoot = attributes.Shoot;

        return weaponData;
    }
    public static void Copy(WeaponDataAttributes from, WeaponDataAttributes other) {
        other.WeaponType = from.WeaponType;
        other.damage = from.damage;
        other.maxDistance = from.maxDistance;
        other.currentAmmo = from.currentAmmo;
        other.backPackAmmo = from.backPackAmmo;
        other.magCapacity = from.magCapacity;
        other.fireRate = from.fireRate;
        other.MagEmpty = from.MagEmpty;
        other.Draw = from.Draw;
        other.Reload = from.Reload;
        other.Shoot = from.Shoot;
    }
}