using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : ButtonClick {
    [SerializeField] private string sceneToLoad = "MainGame";
    [SerializeField] private AudioClip audioClip;
    protected override void HandleRightClick(){
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        SceneManager.LoadScene(sceneToLoad);
    }
    protected override void Update(){
        base.Update();
    }
}