using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour{
    [SerializeField] private AudioClip ambient;
    [Range(0f,1f)] private float Volume = 1f;

    public AudioClip Ambient{
        set { ambient = value; }
        get{ return ambient; }
    }
    void Update(){
        PlayAmbient();
    }
    public void PlayAmbient(){
        SoundFXManager.instance.PlaySoundFXClip(ambient,transform,Volume);
    }

    public void Reset(){
        SoundFXManager.instance.Reset();
    }
}