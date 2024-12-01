using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIneract : MonoBehaviour{
    private Camera Camera;
    private PlayerUI playerUI;
    private InputManager inputManager;
    [SerializeField] private float RayDistance = 3f;
    [SerializeField] private LayerMask layerMask;

    void Start(){
        Camera = GetComponent<PlayerLook>().Camera_;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    void Update(){
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(Camera.transform.position, Camera.transform.forward);
        RaycastHit hitInfo;

        if(!Physics.Raycast(ray, out hitInfo, RayDistance, layerMask) || hitInfo.collider.GetComponent<Interactable>()==null) return;
        Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
        playerUI.UpdateText(interactable.promptMessage);
        
        if(inputManager.onFootActions.Interact.triggered){
            interactable.BaseIneract();
        }
    }
}
