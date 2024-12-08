using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelEnter : MonoBehaviour {
    [SerializeField] private AudioClip[] StartAudio;
    [SerializeField] private float secondsDelay = 1f;

    void Awake(){
        Invoke("PlaySound", secondsDelay);
    }
    private void PlaySound(){
        AudioClip audioClip = StartAudio[Random.Range(0,StartAudio.Length)];
        SoundFXManager.PlaySoundClipForcePlayer(audioClip);
        DestroyThisScript(audioClip.length);
    }
    private void DestroyThisScript(float delay){
        Destroy(this, delay);
    }
}