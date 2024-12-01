using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable{
    [SerializeField] private DoorHandler doorHandler;
    private bool triggert;

    void Start(){
        doorHandler = transform.parent.GetComponent<DoorHandler>();
    }
    protected override void Interact(){
        if(triggert) {
            AlreadyOpened();
            return;
        }
        doorHandler.Open();
        triggert = true;
    }

    private void AlreadyOpened(){
        Debug.Log("The door is already triggerd");
    }
}