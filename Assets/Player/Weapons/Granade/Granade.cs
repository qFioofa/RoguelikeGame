using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : WeaponBehavior {

    [SerializeField] private WeaponHandler weaponHandler;

    void Start(){
        weaponHandler = GameObject.FindWithTag("Player")?.GetComponent<WeaponHandler>();
    }

    public override void Update(){}

    public override void Shoot(){
        animator.SetTrigger("PullIn");
    }

    public override void ShootHandler() {
        animator.SetTrigger("Trow");
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
