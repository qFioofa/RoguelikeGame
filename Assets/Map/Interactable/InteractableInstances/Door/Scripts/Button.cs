using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : Interactable{
    [SerializeField] private DoorHandler doorHandler;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClipTriggerd;
    private bool Triggered;
    void Start(){
        doorHandler = transform.parent.GetComponent<DoorHandler>();
    }
    protected override void Interact(){
        if(Triggered || doorHandler.RoomHandler.isRoomGenerated || !doorHandler.RoomHandler.isRoomCleared) {
            AlreadyOpened();
            return;
        }

        Triggered = true;
        doorHandler.Open();
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
    }

    private void AlreadyOpened(){
        SoundFXManager.PlaySoundClipForce(audioClipTriggerd,transform);
    }
}