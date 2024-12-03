using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBehavier {
    public override void Update(){
        base.Update();
    }
    public override void ShootHandler(){
        animator.SetTrigger("MidSlash");
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
