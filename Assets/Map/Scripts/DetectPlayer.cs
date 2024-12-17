using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {
    void OnCollisionEnter(Collision collision)
       {
           if (collision.gameObject.CompareTag("Player"))
           {
               Debug.Log("Collided with target!");
           }
       }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered with target!");
        }
    }
}