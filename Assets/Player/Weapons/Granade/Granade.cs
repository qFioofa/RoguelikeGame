using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : WeaponBehavior {

    public override void Shoot(){
        animator.SetTrigger("Trow");
    }

    public override void Draw(){
        animator.SetTrigger("Draw");
    }
    public void DrawSound(){
        SoundFXManager.PlaySoundClipForce(weaponData.Draw, transform);
    }
}
