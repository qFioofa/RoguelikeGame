using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    [SerializeField] private static Transform Player;
    private bool isSoundPlaying = false;
    void Awake(){
        if (instance == null) instance = this;
        Player = GameObject.FindWithTag("Player")?.transform;
    }

    public static void PlaySoundClipForce(AudioClip audioClip, Transform spawnTransform, float volume = 1.0f){
        AudioSource audioSource = spawnTransform.gameObject.AddComponent<AudioSource>();
        
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }

    public static void PlaySoundClipForcePlayer(AudioClip audioClip, float volume = 1.0f){
        if(Player == null){
            Debug.Log("SoundXF: Player was not found");
            return;
        }
        AudioSource audioSource = Player.gameObject.AddComponent<AudioSource>();
        
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }


    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume = 1.0f){
        if (isSoundPlaying) return;

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        isSoundPlaying = true;
        Destroy(audioSource.gameObject, audioSource.clip.length);
        StartCoroutine(ResetSoundFlag(audioSource.clip.length));
    }
    private IEnumerator ResetSoundFlag(float delay){
        yield return new WaitForSeconds(delay);
        isSoundPlaying = false;
    }
}

