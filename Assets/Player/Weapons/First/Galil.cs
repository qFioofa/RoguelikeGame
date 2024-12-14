using UnityEngine;

public class Galil : WeaponBehavior {

    private float damage = 10f;
    private float range = 100f;

    [SerializeField] private Camera fpsCamera;

    protected override void Start() {
        base.Start();

    }
    public override void Update(){
        if(isShooting) ShootHandler();
        base.Update();
    }

    public override void Shoot(){
        base.Shoot();
    }
    public override void ShootHandler() {
        if (weaponData.fireRate + LastShotTime > Time.time) return;
        if (weaponData.currentAmmo <= 0) {
            if (!PlayedSoundOnReload) SoundFXManager.PlaySoundClipForcePlayer(weaponData.MagEmpty);
            PlayedSoundOnReload = true;
            return;
        }
        // shooting mechanics
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
        weaponData.currentAmmo = Mathf.Clamp(weaponData.currentAmmo - 1, 0, weaponData.magCapacity);
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

    public void ClipOutSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[0], transform);
    }

    public void ClipInSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[1], transform);
    }

    public void BoltPullSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Reload[2], transform);
    }
}