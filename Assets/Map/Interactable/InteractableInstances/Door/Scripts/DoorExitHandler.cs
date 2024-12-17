using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitHandler : MonoBehaviour{
    [SerializeField] private DoorHandler doorHandler;

    void Start(){
        doorHandler = transform.parent.GetComponent<DoorHandler>();
    }
    private void OnTriggerEnter(Collider other){
        if(!other.CompareTag("Player")) return;
        doorHandler.Close();
        DeleteAllPickUp();
        Destroy(gameObject);
    }

    private void DeleteAllPickUp(){
        PickUp[] pickUps = FindObjectsOfType<PickUp>();
        foreach (PickUp pickUp in pickUps) Destroy(pickUp.gameObject);
    }
}