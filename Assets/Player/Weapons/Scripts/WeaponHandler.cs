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
    [SerializeField] private WeaponType defaultFirstDraw = WeaponType.First;
    [Header("Weapons")]
    [SerializeField] private WeaponBehavior FirstWeapon;
    [SerializeField] private WeaponBehavior SecondWeapon;
    [SerializeField] private WeaponBehavior MeleeWeapon;
    [SerializeField] private WeaponBehavior GranadeWeapon;
    private WeaponType currentWeapon;

    void Start(){
        SetWeapons();
        SetAllActiveFalseWeapons();
        SelectWeaponType(defaultFirstDraw);
    }

    private void SetWeapons(string FirstWeapomTag = "FirstWeapon", string SecondWeaponTag = "SecondWeapon", string MeleeWeaponTag = "MeleeWeapon", string GranadeWeaponTag = "GranadeWeapon"){
        FirstWeapon = GameObject.FindGameObjectWithTag(FirstWeapomTag)?.GetComponent<WeaponBehavior>();
        SecondWeapon = GameObject.FindGameObjectWithTag(SecondWeaponTag)?.GetComponent<WeaponBehavior>();
        MeleeWeapon = GameObject.FindGameObjectWithTag(MeleeWeaponTag)?.GetComponent<WeaponBehavior>();
        GranadeWeapon = GameObject.FindGameObjectWithTag(GranadeWeaponTag)?.GetComponent<WeaponBehavior>();
    }

    private void SelectWeaponType(WeaponType wType) {

        if(currentWeapon == wType) return;

        SetAllActiveFalseWeapons();

        switch(wType){
            case WeaponType.First:
                FirstWeapon?.SetActive(true);
                FirstWeapon.Draw();
                break;
            case WeaponType.Second:
                SecondWeapon?.SetActive(true);
                SecondWeapon.Draw();
                break;
            case WeaponType.Melee:
                MeleeWeapon?.SetActive(true);
                MeleeWeapon.Draw();
                break;
            case WeaponType.Granade:
                GranadeWeapon?.SetActive(true);
                GranadeWeapon.Draw();
                break;
        }

        currentWeapon = wType;
    }

    private void ReloadWeapon(){
        switch(currentWeapon){
            case WeaponType.First:
                FirstWeapon.Reload();
                break;
            case WeaponType.Second:
                SecondWeapon.Reload();
                break;
            case WeaponType.Melee:
                MeleeWeapon.Reload();
                break;
            case WeaponType.Granade:
                GranadeWeapon.Reload();
                break;
        }
    }

    private void ShootWeapon(){
        switch(currentWeapon){
            case WeaponType.First:
                FirstWeapon.Shoot();
                break;
            case WeaponType.Second:
                SecondWeapon.Shoot();
                break;
            case WeaponType.Melee:
                MeleeWeapon.Shoot();
                break;
            case WeaponType.Granade:
                GranadeWeapon.Shoot();
                break;
        }
    }

    private void ShootCancelWeapon(){
        switch(currentWeapon){
            case WeaponType.First:
                FirstWeapon.ShootCancel();
                break;
            case WeaponType.Second:
                SecondWeapon.ShootCancel();
                break;
            case WeaponType.Melee:
                MeleeWeapon.ShootCancel();
                break;
            case WeaponType.Granade:
                GranadeWeapon.ShootCancel();
                break;
        }
    }

    private void SetAllActiveFalseWeapons(){
        FirstWeapon?.SetActive(false);
        SecondWeapon?.SetActive(false);
        MeleeWeapon?.SetActive(false);
        GranadeWeapon?.SetActive(false);
    }

    public void First(){
        SelectWeaponType(WeaponType.First);
    }

    public void Second(){
        SelectWeaponType(WeaponType.Second);
    }

    public void Melee(){
        SelectWeaponType(WeaponType.Melee);
    }

    public void Granade(){
        SelectWeaponType(WeaponType.Granade);
    }

    public void Reload(){
        ReloadWeapon();
    }

    public void Shoot(){
        ShootWeapon();
    }

    public void ShootCancel(){
        ShootCancelWeapon();
    }

    public void AddOneMag(){
        FirstWeapon.AddOneMag();
        SecondWeapon.AddOneMag();
        MeleeWeapon.AddOneMag();
    }
}
