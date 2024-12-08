using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked : ButtonClick { 
    [SerializeField] private AudioClip audioClip;
    private Transform cam;
    void Start(){
        cam = GameObject.FindWithTag("MainCamera").transform;
    }
    protected override void HandleRightClick(){
        SoundFXManager.PlaySoundClipForce(audioClip,cam);
    }
}
