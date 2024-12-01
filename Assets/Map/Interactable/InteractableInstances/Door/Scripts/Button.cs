using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable{
    [SerializeField] private DoorHandler doorHandler;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private AudioClip audioClipTriggerd;
    private bool triggert;

    void Start(){
        doorHandler = transform.parent.GetComponent<DoorHandler>();
    }
    protected override void Interact(){
        if(triggert) {
            AlreadyOpened();
            return;
        }
        doorHandler.Open();
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        triggert = true;
    }

    private void AlreadyOpened(){
        SoundFXManager.PlaySoundClipForce(audioClipTriggerd,transform);
        Debug.Log("The door is already triggerd");
    }
}