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
        Destroy(gameObject);
    }
}