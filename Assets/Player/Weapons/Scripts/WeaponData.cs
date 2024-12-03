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
}