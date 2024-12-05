using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour {
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Animator animator;
    protected bool isShooting;
    protected bool isReloading;
    protected float LastShotTime;

    public bool IsShooting {
        get { return isShooting; }
    }

    public bool IsReloading {
        get { return isReloading; }
    }

    void Start() {
        LastShotTime = Time.time;
        animator = GetComponent<Animator>();
    }

    public virtual void Update() {}

    public virtual void Shoot() {
        isShooting = true;
    }

    public virtual void ShootHandler() {}

    public virtual void ShootCancel() {
        isShooting = false;
    }

    public virtual void Reload() {}

    public virtual void ReloadHandler() {
        int needToFill = Mathf.Clamp(weaponData.magCapacity - weaponData.currentAmmo, 0, weaponData.magCapacity);
        int canFill = Mathf.Min(needToFill, weaponData.backPackAmmo);

        weaponData.currentAmmo += canFill;
        weaponData.backPackAmmo -= canFill;
    }

    public virtual void ReloadDone() {
        ReloadHandler();
    }

    public virtual void AddOneMag() {
        weaponData.backPackAmmo += weaponData.magCapacity;
        Debug.Log($"Added ammo. Backpack ammo: {weaponData.backPackAmmo}, Current ammo: {weaponData.currentAmmo}, Magazine capacity: {weaponData.magCapacity}");
    }

    public virtual void Draw() {}

    public virtual void SetActive(bool active) {
        gameObject.SetActive(active);
    }

    public virtual bool CanShoot() {
        return weaponData.currentAmmo > 0 && !isReloading && weaponData.fireRate + LastShotTime <=Time.time;
    }

    public virtual void ResetAnimatorSpeed(){}
}