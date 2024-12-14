using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player_Info", menuName = "Player/Player_Info")]
public class PlayerInfo : MonoBehaviour {
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioClip die; //sound when die
    [SerializeField] private Image bloodOverlay; //screen when low hp - did not work
    private WeaponHandler weaponHandler;
    private bool isFalling = false; // fall when dead

    void Start() {
        weaponHandler = GetComponent<WeaponHandler>();
    }

    public int Money {
        get { return playerData.Money; }
    }

    public float Health{
        get { return playerData.Health; }
    }

    public void AddOneMag() {
        weaponHandler.AddOneMag();
    }

    public bool IsDead() => playerData.Health <= 0;
    public void AddMoney(int money) => playerData.Money+=money;
    public void SpendMoney(int money) => playerData.Money=playerData.Money-money > 0 ? playerData.Money-money : 0;
    public void AddHealth(int health) {
        playerData.Health = health + playerData.Health >= playerData.HealthLimit ? playerData.HealthLimit : health + playerData.Health;
        if (playerData.Health > playerData.HealthLimit * 0.25)
        {
            HideBloodOverlay(); // turn off screen with blood
        }
    } 
    public void Damage(int received_damage)
    {
        playerData.Health -= received_damage;
        if (playerData.Health <=  playerData.HealthLimit * 0.25)
        {
            ShowBloodOverlay(); // turn on screen with blood
        }
        if (playerData.Health <= 0f)
        {
            Die();
        }
    }

    public void Die() // turn off player and camera movement after death
    {
        CameraFall();
        SoundFXManager.PlaySoundClipForcePlayer(die);
        //ShowResults(); // statistics of level in canvas
    }
    public void CameraFall()
    {
        if (!isFalling)
        {
            isFalling = true;
            StartCoroutine(FallCoroutine());
        }
    }
    private System.Collections.IEnumerator FallCoroutine()
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(40f, -45f, 30f);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * 1.5f;
            yield return null;
        }
        transform.rotation = targetRotation;
        isFalling = false;
    }

    private void ShowBloodOverlay()  // make turning on Image with blood - did not work right now
    {
        if (bloodOverlay != null)
        {
            bloodOverlay.enabled = true;
        }
    } 

    private void HideBloodOverlay() // make turning off Image with blood - did not work right now
    {
        if (bloodOverlay != null)
        {
            bloodOverlay.enabled = false;
        }
    } 
    public void ShowResults() { } // make statistics
}
