using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : ButtonClick {
    [SerializeField] private string sceneToLoad = "MainGame";
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Camera mianCamera;

    [Header("Pixel effect")]
    [SerializeField] private PlayerEnterGameEffects playerEnterGameEffects;
    [SerializeField] private float effectDurarion = 10.0f;

    void Start(){
        playerEnterGameEffects = mianCamera.GetComponent<PlayerEnterGameEffects>();
    }
    protected override void HandleRightClick(){
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        StartCoroutine(playerEnterGameEffects.TogglePixelation(0.7f, 0.05f, effectDurarion));
        Invoke("LoadMainGame", 3.1f);
    }
    protected override void Update(){
        base.Update();
    }
    private void LoadMainGame(){
        SceneManager.LoadScene(sceneToLoad);
    }
}