using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour{
    private readonly string IsOpenTag = "IsOpen";
    [SerializeField] private Transform exit;
    [SerializeField] private AudioClip audioClip;
    private RoomHandler roomHandler;
    private Animator animator;
    private bool IsOpen;
    public bool isOpen{
        get{ return IsOpen; }
        set{ IsOpen = value; }
    }
    public RoomHandler RoomHandler{
        get { return roomHandler; }
    }
    private bool GeneratedRoom;
    void Start(){
        animator = GetComponent<Animator>();
        getRoomHandler();
        InitExit();
    }
    public void Open(){
        isOpen = true;
        GenerateRoom();
        Animations();
    }
    public void Close(){
        isOpen = false;
        roomHandler.isRoomCleared = false;
        Animations();
        DeletePrevRoom();
    }
    public void DeletePrevRoom(){
        roomHandler.DeletePrevRoom();
    }
    private void GenerateRoom(){
        roomHandler.generateNextRoom(exit);
    }

    private void Animations(){
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        animator.SetBool(IsOpenTag,IsOpen);
    }

    private void getRoomHandler(string RoomWrapperTag = "RoomWrapper"){
        roomHandler = GameObject.FindGameObjectWithTag(RoomWrapperTag).GetComponent<RoomHandler>();
    }

    private void InitExit(string DoorExitTag = "DoorExit"){
        foreach(Transform inst in transform){
            if(inst.CompareTag(DoorExitTag)){
                exit = inst;
                return;
            }
        }
        exit = null;
    }
}
