using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public AudioClip pickUpSound;
    public PlayerInfo playerInfo;

    protected virtual void Start(){
        playerInfo = GameObject.FindWithTag("Player")?.GetComponent<PlayerInfo>();
    }

    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            PlaySound();
            PickedUp();
        }
    }

    protected virtual void PickedUp(){}

    protected virtual void Destroy(){
        Destroy(gameObject);
    }

    protected virtual void PlaySound(){
        SoundFXManager.PlaySoundClipForcePlayer(pickUpSound);
    }
}
