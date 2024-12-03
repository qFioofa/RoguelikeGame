using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponBehavier : MonoBehaviour {
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Animator animator;
    protected bool isShooting;
    protected float fireRateTimePassed = 0;
    protected bool isReloading;
    public bool IsShooting{
        get{ return isShooting; }
    }
    public bool IsReloading{
        get{ return isReloading; }
    }

    public virtual void Update() {
        fireRateTimePassed += Time.deltaTime;

        if (isShooting) ShootHandler();
    }

    void Start(){
        animator = GetComponent<Animator>();
    }

    public virtual void Shoot(){
        if(!CanShoot()) {
            SoundFXManager.PlaySoundClipForce(weaponData.MagEmpty, transform);
            return;
        }
        isShooting = true;
    }

    public virtual void ShootHandler() {
        Debug.Log(weaponData.currentAmmo);
        weaponData.currentAmmo = Mathf.Clamp(weaponData.currentAmmo-1, 0, weaponData.magCapacity);
        Debug.Log(weaponData.currentAmmo);
    }

    public virtual void ShootCancel(){
        isShooting = false;
    }

    public virtual void Reload(){}

    public virtual void ReloadHandler() {
        // Calculate how much ammo is needed to fill the magazine
        int needToFill = Mathf.Clamp(weaponData.magCapacity - weaponData.currentAmmo, 0, weaponData.magCapacity);
        
        // Check how much ammo can actually be filled from the backpack
        int canFill = Mathf.Min(needToFill, weaponData.backPackAmmo);

        // Update the current ammo and the backpack ammo
        weaponData.currentAmmo += canFill;
        weaponData.backPackAmmo -= canFill;
    }



    public virtual void ReloadDone() {
        ReloadHandler();
    }

    public virtual void Draw(){}

    public virtual void SetActive(bool active) {
        gameObject.SetActive(active);
    }

    public virtual bool CanShoot() {
        return weaponData.currentAmmo > 0 && !isReloading && fireRateTimePassed >= weaponData.fireRate;
    }
}