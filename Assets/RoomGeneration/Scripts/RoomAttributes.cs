using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RoomAttributes : MonoBehaviour{
    [SerializeField] private Transform entry;
    [SerializeField] private Transform exit;

    public Transform Entry{
        get {return entry;}
    }

    public Transform Exit{
        get {return exit;}
    }

    void Start(){
        InitEntry();
        InitExit();
    }

    public void Setup(){
        Start();
    }

    public void InitEntry(string entryTag = "RoomEntry"){
        foreach(Transform inst in transform){
            if(inst.CompareTag(entryTag)){
                entry = inst;
                return;
            }
        }
        entry = null;
    }

    public void InitExit(string exitTag = "RoomExit"){
        foreach(Transform inst in transform){
            if(inst.CompareTag(exitTag)){
                exit = inst;
                return;
            }
        }
        exit = null;
    }


}