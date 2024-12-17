using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int totalEnemies; // Общее количество врагов
    private int enemiesKilled; // Количество убитых врагов
    private RoomHandler roomHandler; // Ссылка на RoomHandler

    void Start()
    {
        roomHandler = FindObjectOfType<RoomHandler>(); // Находим RoomHandler в сцене
        enemiesKilled = 0; // Инициализируем количество убитых врагов
    }

    public void SetTotalEnemies(int count)
    {
        totalEnemies = count; // Устанавливаем общее количество врагов
        Debug.Log($"Всего врагов: {totalEnemies}");
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        CheckIfRoomCleared();
    }

    private void CheckIfRoomCleared()
    {
        if (enemiesKilled >= totalEnemies)
        {
            roomHandler.isRoomCleaned = true; // Устанавливаем RoomCleared в true
            Debug.Log("Все враги убиты! Комната очищена.");
        }
    }
}
