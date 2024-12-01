using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour{
    private readonly string IsOpenTag = "IsOpen";
    [SerializeField] private Transform exit;
    [SerializeField] private AudioClip audioClip;
    private RoomHandler roomHandler;
    private bool IsOpen;
    public bool isOpen{
        get{ return IsOpen; }
        set{ IsOpen = value; }
    }
    private bool GeneratedRoom;
    void Start(){
        getRoomHandler();
        InitExit();
    }
    public bool Open(){
        isOpen = true;
        GenerateRoom();
        Animations();
        return isOpen;
    }

    public bool Close(){
        isOpen = false;
        Animations();
        DeletePrevRoom();
        return isOpen;
    }
    public void DeletePrevRoom(){
        roomHandler.DeletePrevRoom();
    }
    private void GenerateRoom(){
        if(GeneratedRoom) return;
        roomHandler.generateNextRoom(exit);
    }

    private void Animations(){
        SoundFXManager.PlaySoundClipForce(audioClip,transform);
        transform.GetComponent<Animator>().SetBool(IsOpenTag,IsOpen);
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
