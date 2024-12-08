using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayRandomSounds : MonoBehaviour {
    [SerializeField] private AudioClip[] audioClips;
    [Header("Settings")]
    [SerializeField] private float minTimeDelay = 13f;
    [SerializeField] private float minRandomRange = 0f;
    [SerializeField] private float maxRandomRange = 7f;
    private float lastTime = 0f;
    private float nextAudioTime = 0f;

    void Update(){
        Prosecc();
    }

    private void Prosecc(){
        lastTime += Time.deltaTime;
        if(lastTime < nextAudioTime) return;
        lastTime = 0;
        SetNextAudioTime();
        SoundFXManager.PlaySoundClipForcePlayer(GetRadnomAudio());
    }

    private void SetNextAudioTime(){
        nextAudioTime = minTimeDelay + Random.Range(minRandomRange, maxRandomRange);
    }

    private AudioClip GetRadnomAudio(){
        return audioClips[Random.Range(0,audioClips.Length)];
    }

}
