using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    First,
    Second,
    Melee,
    Granade
}

public class WeaponHandler : MonoBehaviour {

    [Header("Start")]
    [SerializeField] private WeaponType defaultFirstDraw = WeaponType.Melee;

    public WeaponType DefaultFirstDraw{
        get { return defaultFirstDraw; }
    }
    [Header("Weapons")]
    [SerializeField]
    private Dictionary<WeaponType, WeaponBehavior> weaponsMap = new Dictionary<WeaponType, WeaponBehavior>();

    private WeaponType currentWeapon;

    void Start() {
        SetWeapons();
        SetAllActiveFalseWeapons();
        SelectWeaponTypeForce(defaultFirstDraw);
    }

    private void SetWeapons() {
        weaponsMap[WeaponType.First] = GameObject.FindGameObjectWithTag("FirstWeapon")?.GetComponent<WeaponBehavior>();
        weaponsMap[WeaponType.Second] = GameObject.FindGameObjectWithTag("SecondWeapon")?.GetComponent<WeaponBehavior>();
        weaponsMap[WeaponType.Melee] = GameObject.FindGameObjectWithTag("MeleeWeapon")?.GetComponent<WeaponBehavior>();
        weaponsMap[WeaponType.Granade] = GameObject.FindGameObjectWithTag("GranadeWeapon")?.GetComponent<WeaponBehavior>();
    }

    private void SelectWeaponType(WeaponType wType) {
        if (currentWeapon == wType) return;

        SetAllActiveFalseWeapons();

        if (weaponsMap.TryGetValue(wType, out WeaponBehavior weapon)) {
            if (wType == WeaponType.Granade) {
                WeaponData weaponData = weapon?.WeaponData;
                if (weaponData == null || weaponData.currentAmmo <= 0) {
                    SoundFXManager.PlaySoundClipForce(weaponData.MagEmpty, transform);
                    SelectWeaponTypeForce(defaultFirstDraw);
                    return;
                }
            }
            weapon?.SetActive(true);
            weapon?.Draw();
        }

        currentWeapon = wType;
    }

    public void SelectWeaponTypeForce(WeaponType wType) {

        SetAllActiveFalseWeapons();

        if (weaponsMap.TryGetValue(wType, out WeaponBehavior weapon)) {
            if (wType == WeaponType.Granade) {
                WeaponData weaponData = weapon?.WeaponData;
                if (weaponData == null || weaponData.currentAmmo <= 0) {
                    SoundFXManager.PlaySoundClipForce(weaponData.MagEmpty, transform);
                    SelectWeaponTypeForce(defaultFirstDraw);
                    return;
                }
            }
            weapon?.SetActive(true);
            weapon?.Draw();
        }

        currentWeapon = wType;
    }


    private void ReloadWeapon(){
        if (weaponsMap.TryGetValue(currentWeapon, out WeaponBehavior weapon)) {
            weapon?.Reload();
        }
    }

    private void ShootWeapon(){
        if (weaponsMap.TryGetValue(currentWeapon, out WeaponBehavior weapon)) {
            weapon?.Shoot();
        }
    }

    private void ShootCancelWeapon(){
        if (weaponsMap.TryGetValue(currentWeapon, out WeaponBehavior weapon)) {
            weapon?.ShootCancel();
        }
    }

    private void SetAllActiveFalseWeapons(){
        weaponsMap[WeaponType.First]?.SetActive(false);
        weaponsMap[WeaponType.Second]?.SetActive(false);
        weaponsMap[WeaponType.Melee]?.SetActive(false);
        weaponsMap[WeaponType.Granade]?.SetActive(false);
    }

    public void First() => SelectWeaponType(WeaponType.First);

    public void Second() => SelectWeaponType(WeaponType.Second);

    public void Melee() => SelectWeaponType(WeaponType.Melee);

    public void Granade() => SelectWeaponType(WeaponType.Granade);

    public void Reload() => ReloadWeapon();

    public void Shoot() => ShootWeapon();

    public void ShootCancel() => ShootCancelWeapon();

    public void AddOneMag(){
        weaponsMap[WeaponType.First].AddOneMag();
        weaponsMap[WeaponType.Second].AddOneMag();
    }

    public void AddGranade(int value){
        weaponsMap[WeaponType.Granade].AddAmmo(value);
    }
    public WeaponData GetInfoWeapon(){
        if (weaponsMap.TryGetValue(currentWeapon, out WeaponBehavior weapon)) {
            return weapon?.WeaponData;
        }
        return null;
    }
}
