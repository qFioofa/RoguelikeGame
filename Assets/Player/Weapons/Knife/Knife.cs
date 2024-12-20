using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBehavior {
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
    public override void ShootHandler() {
        if (weaponData.fireRate + LastShotTime > Time.time) return;
        LastShotTime = Time.time;
        animator.speed = 1f / weaponData.fireRate;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.transform.position, Camera.main.transform.transform.forward, out hit, weaponData.maxDistance)) {

            IDamageable target = hit.transform.GetComponent<IDamageable>();
            if (target != null) {
                SoundFXManager.PlaySoundClipForcePlayer(weaponData.Reload[Random.Range(0,weaponData.Reload.Length-1)]);
                target.TakeDamage(weaponData.damage);
            }
        }

        animator.SetTrigger("MidSlash");
        base.ShootHandler();
    }

    public override void ResetAnimatorSpeed(){
        animator.speed = 1f;
    }
    public void MidSlashSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Shoot, transform);
    }
    public override void Draw(){
        animator.SetTrigger("Draw");
    }

    public void DrawSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Draw, transform);
    }
}
