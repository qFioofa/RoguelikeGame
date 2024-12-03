using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour{
    [SerializeField] private Camera Camera;
    public Camera Camera_{
        get{return Camera;}
    }
    private float xRotation = 0f;
    [SerializeField] private float xSensitivity = 10f;
    [SerializeField] private float ySensitivity = 10f;
    [SerializeField] private float upAngleLimit = 80f;
    [SerializeField] private float downAngleLimit = -80f;
    public void ProcessLook(Vector2 input){
        xRotation -= (input.y * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation,downAngleLimit,upAngleLimit);

        Camera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * xSensitivity);
    }
}
