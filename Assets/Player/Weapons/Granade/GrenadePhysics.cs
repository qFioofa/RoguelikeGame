using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePhysics : MonoBehaviour {

    [SerializeField] private GameObject explotionEffect;

    [Header("Audio")]
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private AudioClip[] explotionAudio;

    [Header("Parameters")]
    [SerializeField] private float delay = 3f;
    [SerializeField] private float explotionEffectDelay = 1f;
    [SerializeField] private float radius = 5f;

    private float countDown = 0f;
    private bool hasExploded = false;

    void Start(){
        countDown = delay;
        explotionEffectDelay = explotionAudio.Length;
    }

    void Update(){
        handleTimer();
    }

    private void handleTimer() {
        if(hasExploded) return;
        countDown -= Time.deltaTime;

        if(countDown <=0) {
            handleExplotion();
            hasExploded = true;
        }
    }

    private void handleExplotion(){
        GameObject Effect = Instantiate(explotionEffect, transform.position + new Vector3(0,1f,0) , Quaternion.identity);

        Destroy(Effect, explotionEffectDelay-1f);

        Explotion();

        Destroy(gameObject);
    }

    private void Explotion(){
        SoundFXManager.PlaySoundClipForcePlayer(getExplotionAudio());
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var obj in colliders){
            if(obj.CompareTag("Enemy")){
                Debug.Log("Enemy detected");
            }
        }
    }

    private AudioClip getExplotionAudio(){
        return explotionAudio[Random.Range(0,explotionAudio.Length-1)];
    }

    private void OnCollisionEnter(Collision collision) {
        SoundFXManager.PlaySoundClipForce(collisionAudio, transform);
    }
}
