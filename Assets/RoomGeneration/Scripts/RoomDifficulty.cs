using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDifficulty : MonoBehaviour {
    private WeaponHandler weaponHandler;

    [Header("Multiplayers")]
    [SerializeField] private float dmgMtp = 1.01f;
    [SerializeField] private float hpMtp = 1.01f;
    [SerializeField] private int granadeRecover = 2;
    private EnemyInfo enemyInfo = new EnemyInfo();

    [Header("Sounds")]
    [SerializeField] private AudioClip ammoCover;

    void Start(){
        weaponHandler = GameObject.FindWithTag("Player")?.GetComponent<WeaponHandler>();
    }

    public EnemyInfo GetEnemyInfo(){
        enemyInfo.damage = (int)(enemyInfo.damage * dmgMtp);
        enemyInfo.HP *= hpMtp;
        return enemyInfo;
    }

    public void restoreGranade(){
        SoundFXManager.PlaySoundClipForcePlayer(ammoCover);
        weaponHandler.AddGranade(granadeRecover);
    }

}