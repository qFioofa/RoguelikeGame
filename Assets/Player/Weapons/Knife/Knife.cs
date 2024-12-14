using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBehavior {

    private float damage = 10f;
    private float range = 1f;
    [SerializeField] private Camera fpsCamera;

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

        // hurting mechanics
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyLife target = hit.transform.GetComponent<EnemyLife>(); // EnemyLife - script of enemy, when enemy takes damage
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        LastShotTime = Time.time;
        animator.speed = 1f / weaponData.fireRate;

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
