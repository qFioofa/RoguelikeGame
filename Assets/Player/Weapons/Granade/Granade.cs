using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Granade : WeaponBehavior {
    private WeaponHandler weaponHandler;

    [Header("Physical Granade")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 offset = new Vector3(-0.1f,-0.5f,-1f);
    private Vector3 throwDiraction = new Vector3(0,0.1f,0);
    private Camera _cam;

    [Header("Grenade Parameters")]
    public float throwForce = 30f;
    protected override void Start() {
        base.Start();
        _cam = Camera.main;
        weaponHandler = GameObject.FindWithTag("Player")?.GetComponent<WeaponHandler>();
    }

    protected override void InitStartValues(){
        weaponData.currentAmmo = 2;
    }
    public override void Update(){}

    public override void Shoot(){
        if(weaponData.currentAmmo <=0){
            SoundFXManager.PlaySoundClipForcePlayer(weaponData.MagEmpty);
            return;
        }
        animator.SetTrigger("PullIn");
    }

    public override void ShootHandler() {
        animator.SetTrigger("Trow");
    }

    public override void AddAmmo(int value) {
        weaponData.currentAmmo = value;
    }
    public void InitPythicalGranade(){
        weaponData.currentAmmo = Mathf.Clamp(weaponData.currentAmmo - 1, 0, weaponData.magCapacity);

        Vector3 spawnPosition = transform.position + offset + _cam.transform.forward;

        GameObject granade = Instantiate(prefab, spawnPosition, _cam.transform.rotation);

        granade.GetComponent<GrenadePhysics>().Damage = weaponData.damage;

        Rigidbody rb = granade.GetComponent<Rigidbody>();

        Vector3 dir = (_cam.transform.forward + throwDiraction).normalized;
        rb.AddForce(dir * throwForce, ForceMode.VelocityChange);
    }

    public override void ShootCancel(){
        weaponHandler.SelectWeaponTypeForce(weaponHandler.DefaultFirstDraw);
    }

    public override void Draw(){
        animator.SetTrigger("Draw");
    }
    public void DrawSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Draw, transform);
    }

    public void PinPullSound(){
        SoundFXManager.PlaySoundClipForcePlayer(weaponData.Reload[0]);
    }

    public void ShootSound(){
        SoundFXManager.PlaySoundClipForcePlayer(weaponData.Shoot);
    }
}
