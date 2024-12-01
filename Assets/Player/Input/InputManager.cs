using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;
    private PlayerLook playerLook;
    private PlayerInputHandler playerInputHandler;
    void Awake(){
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLook = GetComponent<PlayerLook>();
        onFootActions.Jump.performed += ctx => playerInputHandler.Jump();
    }

    void FixedUpdate(){
        playerInputHandler.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    void LateUpdate(){
        playerLook.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        onFootActions.Enable();
    }

    private void OnDisable(){
        onFootActions.Disable();
    }
}
