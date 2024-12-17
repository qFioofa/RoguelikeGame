using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public AudioClip pickUpSound;
    public PlayerInfo playerInfo;
    private bool PlayedSound = false;

    protected virtual void Start(){
        playerInfo = GameObject.FindWithTag("Player")?.GetComponent<PlayerInfo>();
    }

    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            PickedUp();
        }
    }

    protected virtual void PickedUp(){
        PlaySound();
        Destroy();
    }

    protected virtual void Destroy(){
        Destroy(gameObject, 0.05f);
    }

    protected virtual void PlaySound(){
        if(!PlayedSound) SoundFXManager.PlaySoundClipForcePlayer(pickUpSound);
        PlayedSound = true;
    }
}
