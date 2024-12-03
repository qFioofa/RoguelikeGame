using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour{
    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    private bool isGrounded;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHight = 3f;
    [SerializeField] private float gravity = -2f;
    [SerializeField] private readonly float ConstGravity = -2;

    [Header("Footstep Sound")]
    [SerializeField] private float footstepInterval = 0.1f;
    [SerializeField] private AudioClip[] footStepSounds;
    private int footStepPtr = 0;
    private float footstepTimer = 0f;

    void Start(){
        controller = GetComponent<CharacterController>();
    }

    void Update(){
        isGrounded = controller.isGrounded;
        HandleFootsteps();
    }

    public void ProcessMove(Vector2 input){
        Vector3 moveDiraction = Vector3.zero;
        moveDiraction.x = input.x;
        moveDiraction.z = input.y;
        controller.Move(transform.TransformDirection(moveDiraction)*speed*Time.deltaTime);
        playerVelocity.y+=gravity*Time.deltaTime;

        if(isGrounded && playerVelocity.y<0){
            playerVelocity.y = ConstGravity;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void  Jump(){
        if(!isGrounded) return;

        playerVelocity.y = Mathf.Sqrt(jumpHight * -3.0f * gravity);
    }

    private void HandleFootsteps(){
        if (isGrounded && controller.velocity.magnitude > 0f){
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= footstepInterval){
                SoundFXManager.PlaySoundClipForce(getAudioStep(),transform);
                footstepTimer = 0f;
            }
        }
        else{
            footstepTimer = 0f;
        }
    }

    private AudioClip getAudioStep(){
        AudioClip audio_r = footStepSounds[footStepPtr++];
        if(footStepPtr>=footStepSounds.Length) footStepPtr = 0;

        return audio_r;
    }

}
