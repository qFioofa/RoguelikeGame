using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p228 : WeaponBehavier {

    public override void Update(){
        base.Update();
    }

    public override void ShootHandler(){
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
