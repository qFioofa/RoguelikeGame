using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject initObject;
    private GameObject objectHolder;
    [SerializeField] private float Period;
    private float timer;

    void Start(){
        Init();
    }

    void Update() {
        if (IsInited()){
            timer = 0;
            return;
        }
        timer += Time.deltaTime;
        if (timer >= Period) Init();
    }

    private bool IsInited() {
        return objectHolder != null;
    }

    private void Init() {
        objectHolder = Instantiate(initObject, spawnPoint.position, initObject.transform.localRotation);
        objectHolder.transform.parent = spawnPoint.transform; 
    }
}

