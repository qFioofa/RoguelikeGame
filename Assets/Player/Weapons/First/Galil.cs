using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galil : WeaponBehavier {

    public override void Update(){
        base.Update();
    }
    public override void ShootHandler(){
        base.ShootHandler();
        animator.SetTrigger("Shoot");
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
        Debug.Log($"{weaponData.backPackAmmo} {weaponData.currentAmmo} {weaponData.magCapacity}");
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