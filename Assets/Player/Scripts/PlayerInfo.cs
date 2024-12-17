using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip[] dmgTakenSounds;

    [SerializeField] private AudioClip deadSound;
    [Header("Player")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private DeathControler deathControler;
    private WeaponHandler weaponHandler;
    private Transform mainCameraTransform;
    private Rigidbody rigidbodyCamera;
    private bool isDead = false;

    void Start() {
        weaponHandler = GetComponent<WeaponHandler>();
        mainCameraTransform = Camera.main.transform;
        rigidbodyCamera = Camera.main.GetComponent<Rigidbody>();
    }

    public int Money {
        get { return playerData.Money; }
    }

    public float Health {
        get { return playerData.Health; }
    }

    public void AddOneMag() {
        weaponHandler.AddOneMag();
    }

    public void AddMoney(int money) => playerData.Money += money;
    public void SpendMoney(int money) => playerData.Money = playerData.Money - money > 0 ? playerData.Money - money : 0;
    public void AddHealth(int health) => playerData.Health = health + playerData.Health >= playerData.HealthLimit ? playerData.HealthLimit : health + playerData.Health;

    public void Damage(int health) {
        SoundFXManager.PlaySoundClipForcePlayer(dmgTakenSounds[Random.Range(0, dmgTakenSounds.Length - 1)]);
        playerData.Health -= health;
        if (IsDead()) {
            playerData.Health = 0;
            OnPlayerDeath();
        }
    }

    private void OnPlayerDeath() {
        if(isDead) return;
        isDead = true;
        GetComponent<InputManager>().OnDisable();
        deathControler.ShowStats();
        SoundFXManager.PlaySoundClipForcePlayer(deadSound); 
        DetachAndDropCamera(); 
    } 
    private void DetachAndDropCamera() {
        Quaternion rotation =  mainCameraTransform.localRotation;
        Destroy(GameObject.FindWithTag("WeaponCamera"));
        mainCameraTransform.SetParent(null);
        mainCameraTransform.rotation = rotation;
        rigidbodyCamera.isKinematic = false; 
        rigidbodyCamera.AddForce(new Vector3(0, 0.05f, 0), ForceMode.Acceleration);
    }
    public bool IsDead() => playerData.Health <= 0;
}
