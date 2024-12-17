using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticCollecter : MonoBehaviour {

    [Header("Lvl")]
    private int enemyKilled = 0;
    private int moneyEarned = 0;
    private int lvlCompleted = 0;

    [Header("Health")]
    private int healthUsed = 0;
    private float healthCoverd = 0;

    private void UpdateMoneyEarned(int value) { 
        moneyEarned += value; 
    }
    private void UpdateEnemyKilled(int value){
        enemyKilled += value;
    }
    private void UpdateLvlCompleted(int value){
        lvlCompleted += value;
    }

    private void UpdateHealth(int value){
        ++healthUsed;
        healthCoverd += value;
    }
    public string getStatistic(){
        return $"\n\nИгра закончена\n\n\nСтатистика:\nПройдено уровней: {lvlCompleted}\nУбито врагов: {enemyKilled}\nДенег заработано: {moneyEarned}$\nИспользовано аптечек: {healthUsed}.\n Хп вылечино: {healthCoverd}";
    }

    private void OnEnable() {
        HealPickUp.OnHealthPickedUp.AddListener(UpdateHealth); 
        MoneyPickUp.OnMoneyPickedUp.AddListener(UpdateMoneyEarned); 
        IDamageable.DeadCount.AddListener(UpdateEnemyKilled);
    }
    private void OnDisable() { 
        HealPickUp.OnHealthPickedUp.RemoveListener(UpdateHealth); 
        MoneyPickUp.OnMoneyPickedUp.RemoveListener(UpdateMoneyEarned);
        IDamageable.DeadCount.RemoveListener(UpdateEnemyKilled); 
    } 
}