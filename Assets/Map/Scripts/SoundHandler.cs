using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour{
    [SerializeField] private AudioClip ambient;
    [Range(0f,1f)] private float Volume = 1f;
    void Update(){
        SoundFXManager.instance.PlaySoundFXClip(ambient,transform,Volume);
    }
}