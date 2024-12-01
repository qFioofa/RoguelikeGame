using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonClick{
    [SerializeField] private AudioClip audioClip;
    protected override void HandleRightClick(){
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        Application.Quit();
    }
    protected override void Update(){
        base.Update();
    }
}