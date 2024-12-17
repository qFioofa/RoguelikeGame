using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p228 : WeaponBehavior {
    [SerializeField] private BulletTrailEffect bulletTrailEffect;
    public override void Update(){
        if(isShooting) ShootHandler();
        base.Update();
    }

    protected override void Start() {
        base.Start();
    }

    public override void Shoot(){
        base.Shoot();
    }
    public override void ShootHandler(){
        if(weaponData.fireRate + LastShotTime >Time.time) return;
        if(weaponData.currentAmmo <= 0){
            if (!PlayedSoundOnReload) SoundFXManager.PlaySoundClipForcePlayer(weaponData.MagEmpty);
            PlayedSoundOnReload = true;
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.transform.forward, out hit, weaponData.maxDistance)) {
            IDamageable target = hit.transform.GetComponentInParent<IDamageable>();
            if (target != null) target.TakeDamage(weaponData.damage);
        }

        Vector3 startPoint = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;
        Vector3 endPoint = startPoint + direction * weaponData.maxDistance;
        bulletTrailEffect.CreateBulletTrail(startPoint, endPoint);

        weaponData.currentAmmo = Mathf.Clamp(weaponData.currentAmmo-1,0, weaponData.magCapacity);
        LastShotTime = Time.time;
        animator.speed = 1f / weaponData.fireRate;

        animator.SetTrigger("Shoot");
        base.ShootHandler();
    }

    public override void ResetAnimatorSpeed(){
        animator.speed = 1f;
    }

    public void ShootSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Shoot, transform);
    }

    public override void Draw(){
        animator.SetTrigger("Draw");
    }

    public void DrawSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Draw, transform);
    }

    public override void Reload(){
        if (weaponData.backPackAmmo <= 0 || weaponData.currentAmmo >= weaponData.magCapacity) {
            SoundFXManager.PlaySoundClipForce(weaponData.MagEmpty, transform);
            return;
        }
        animator.SetTrigger("Reload");
    }

    public void SlideBackSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[0], transform);
    }
    public void ClipOutSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[1], transform);
    }

    public void ClipInSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[2], transform);
    }

    public void SlideReleaseSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[3], transform);
    }
}
