using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;
    public PlayerInput.WeaponsActions weaponsActions;
    private PlayerLook playerLook;
    private PlayerInputHandler playerInputHandler;
    private WeaponHandler weaponHandler;
    void Awake(){
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;
        weaponsActions = playerInput.Weapons;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        weaponHandler = GetComponent<WeaponHandler>();
        playerLook = GetComponent<PlayerLook>();
        onFootActions.Jump.performed += ctx => playerInputHandler.Jump();

        weaponsActions.First.performed += ctx => weaponHandler.First();
        weaponsActions.Second.performed += ctx => weaponHandler.Second();
        weaponsActions.Melee.performed += ctx => weaponHandler.Melee();
        weaponsActions.Granade.performed += ctx => weaponHandler.Granade();
        weaponsActions.Reload.performed += ctx => weaponHandler.Reload();
        weaponsActions.Shoot.performed += ctx => weaponHandler.Shoot();
        weaponsActions.Shoot.canceled += ctx => weaponHandler.ShootCancel();
    }

    void FixedUpdate(){
        playerInputHandler.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    void LateUpdate(){
        playerLook.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    public void OnEnable(){
        onFootActions.Enable();
        weaponsActions.Enable();
    }

    public void OnDisable(){
        onFootActions.Disable();
        weaponsActions.Disable();
    }
}
