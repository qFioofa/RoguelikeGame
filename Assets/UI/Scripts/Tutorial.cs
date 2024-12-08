using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour {
    [SerializeField] private string sceneToLoad = "Tutorial";
    [SerializeField] private AudioClip audioClip;
    public void LoadTutorialScene() {
        Invoke("LoadSnece", audioClip.length);
    }

    private void LoadSnece(){
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
