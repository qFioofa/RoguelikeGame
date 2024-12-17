using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyTypeA; 
    public GameObject enemyTypeB; 
    public int numberOfEnemiesTypeA = 4; 
    public int numberOfEnemiesTypeB = 3; 
    private Transform[] spawnPoints; 
    private HashSet<int> occupiedSpawnPoints; 
    private EnemyManager enemyManager; // Ссылка на EnemyManager

    void Start()
    {
        spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        occupiedSpawnPoints = new HashSet<int>(); 

        enemyManager = FindObjectOfType<EnemyManager>(); // Находим EnemyManager

        SpawnEnemies(enemyTypeA, numberOfEnemiesTypeA);
        SpawnEnemies(enemyTypeB, numberOfEnemiesTypeB);
        
        // Уведомляем EnemyManager о количестве врагов
        if (enemyManager != null)
        {
            enemyManager.SetTotalEnemies(numberOfEnemiesTypeA + numberOfEnemiesTypeB);
        }
    }

    void SpawnEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (occupiedSpawnPoints.Count >= spawnPoints.Length - 1) 
            {
                break;
            }

            int randomIndex;
            do
            {
                randomIndex = Random.Range(1, spawnPoints.Length); 
            } while (occupiedSpawnPoints.Contains(randomIndex));

            occupiedSpawnPoints.Add(randomIndex); 
            Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
}
