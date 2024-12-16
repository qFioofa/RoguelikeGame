using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDifficulty : MonoBehaviour {
    private RoomHandler roomHandler;
    private WeaponHandler weaponHandler;

    [Header("Multiplayers")]
    [SerializeField] private float dmgMtp = 1.01f;
    [SerializeField] private float hpMtp = 1.01f;


    private int roomsCleaned = 0;



    void Start(){
        roomHandler = GetComponent<RoomHandler>();
    }

    private void Spawn() {

    }

    private void restoreGranade(){

    }


}