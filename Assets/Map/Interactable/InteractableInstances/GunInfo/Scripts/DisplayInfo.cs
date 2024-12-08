using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInfo : Interactable {
    [SerializeField] private AudioClip audioClipTriggerd;
    protected override void Interact(){
        SoundFXManager.PlaySoundClipForce(audioClipTriggerd,transform);
    }
}