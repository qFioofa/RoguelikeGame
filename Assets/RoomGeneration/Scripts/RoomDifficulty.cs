using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDifficulty : MonoBehaviour {
    private WeaponHandler weaponHandler;
    private StatisticCollecter statisticCollecter;
    private RoomHandler roomHandler;

    [Header("Multiplayers")]
    [SerializeField] private float dmgMtp = 1.2f;
    [SerializeField] private float hpAdd = 5;
    [SerializeField] private float hp10lvl = 10;
    [SerializeField] private int granadeRecover = 2;
    private EnemyInfo enemyInfo = new EnemyInfo();

    [Header("Sounds")]
    [SerializeField] private AudioClip ammoCover;

    void Start(){
        weaponHandler = GameObject.FindWithTag("Player")?.GetComponent<WeaponHandler>();
        statisticCollecter = GameObject.FindWithTag("Statistics")?.GetComponent<StatisticCollecter>();
        roomHandler = GetComponent<RoomHandler>();
    }

    public EnemyInfo GetEnemyInfo(){
        enemyInfo.damage = (int)(enemyInfo.damage * dmgMtp);
        enemyInfo.HP += hpAdd;
        if(statisticCollecter.LvlCompleted % 10 == 0){
            enemyInfo.HP += hp10lvl;
        }
        if(statisticCollecter.LvlCompleted % 3 == 0){
            roomHandler.numberOfEnemiesTypeA +=1;
            roomHandler.numberOfEnemiesTypeB +=1;
        }
        return enemyInfo;
    }

    public void restoreGranade(){
        SoundFXManager.PlaySoundClipForcePlayer(ammoCover);
        weaponHandler.AddGranade(granadeRecover);
    }

}