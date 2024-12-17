using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour{
    private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    private bool isGrounded;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHight = 3f;
    [SerializeField] private float gravity = -2f;
    [SerializeField] private readonly float ConstGravity = -2;

    [Header("Sound handler")]
    [SerializeField] private float baseStepInterval = 0.4f;

    [Header("Material for sound")]
    [SerializeField] private AudioClip[] sandSound;
    private int SandPovit = 0;
    [SerializeField] private AudioClip[] MetalSound;
    private int MetalPovit = 0;

    [SerializeField] private AudioClip[] WoodSound;
    private int WoodPovit = 0;

    [SerializeField] private AudioClip[] StoneSound;
    private int StonePovit = 0;

    [SerializeField] private AudioClip[] UnknownSound;
    private int UnknownPovit = 0;

    private float stepTimer = 0;

    void Start(){
        controller = GetComponent<CharacterController>();
    }

    void Update(){
        isGrounded = controller.isGrounded;
        ProcessGravity();
    }

    public void ProcessMove(Vector2 input){
        if (input == Vector2.zero) return;
        HandleSound();

        Vector3 moveDiraction = Vector3.zero;
        moveDiraction.x = input.x;
        moveDiraction.z = input.y;
        controller.Move(transform.TransformDirection(moveDiraction)*speed*Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ProcessGravity(){
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

    private void HandleSound(){
        if(!isGrounded) return;
        stepTimer -= Time.deltaTime;
        if (stepTimer > 0) return;
        
        PlayFootstepSound();
        stepTimer = baseStepInterval;
    }

    private void PlayFootstepSound() {
        AudioClip selectedClip = DetermineSurfaceSound();
        if (selectedClip != null) SoundFXManager.PlaySoundClipForcePlayer(selectedClip);
    }

    private AudioClip DetermineSurfaceSound() {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f)) {
            if (hit.collider == null) return UnknownSound[GetUnknownPovit()];

            switch(hit.collider.tag){
                case "Sand": return sandSound[GetSandPovit()];
                case "Metal": return MetalSound[GetMetalPovit()];
                case "Wood": return WoodSound[GetWoodPovit()];
                case "Stone": return StoneSound[GetStonePovit()];
            }
        }
        return UnknownSound[GetUnknownPovit()];
    }

    private int GetSandPovit()=> ++SandPovit % sandSound.Length;
    private int GetMetalPovit() => ++MetalPovit % MetalSound.Length;
    private int GetWoodPovit() => ++WoodPovit % WoodSound.Length;
    private int GetStonePovit() => ++StonePovit % StoneSound.Length;
    private int GetUnknownPovit() => ++UnknownPovit % UnknownSound.Length;
}
