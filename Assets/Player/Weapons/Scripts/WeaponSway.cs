using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour{
    [Header("Sway settings")]
    [SerializeField] private float smooth = 8f;
    [SerializeField] private float swayMultiplier = 2f;

    void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X")*swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y")*swayMultiplier;

        Quaternion rotX = Quaternion.AngleAxis(-mouseY,Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(mouseX,Vector3.up);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotX*rotY, smooth*Time.deltaTime);
    }
}
