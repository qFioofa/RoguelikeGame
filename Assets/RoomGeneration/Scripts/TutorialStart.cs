using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialStart : MonoBehaviour {
    [SerializeField] private GameObject enemyTypeA; 
    [SerializeField] private GameObject enemyTypeB;
    private Transform[] spawnPoints; 
    private HashSet<int> occupiedSpawnPoints;
    [SerializeField] private int startEnemies = 2;
    void Start(){
        NavMeshSurface surface = GetComponent<NavMeshSurface>();
        if (surface != null) surface.BuildNavMesh();
        occupiedSpawnPoints = new HashSet<int>();
        spawnPoints = transform.Find("SpawnPoints")?.GetComponentsInChildren<Transform>();
        for(int i=0;i<startEnemies; i++) Spawn(0);
    }

    private void Spawn(int _){
        occupiedSpawnPoints = new HashSet<int>();
        if(Random.value<=0.5) Generate(enemyTypeA, 1);
        else Generate(enemyTypeB, 1);
    }

    void Generate(GameObject enemyPrefab, int count) {
        for (int i = 0; i < count; i++) {
            if (occupiedSpawnPoints.Count >= spawnPoints.Length - 1) break;

            int randomIndex;
            do {
                randomIndex = Random.Range(1, spawnPoints.Length-1); 
            } while (occupiedSpawnPoints.Contains(randomIndex));

            occupiedSpawnPoints.Add(randomIndex); 
            Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
    private void OnEnable() {
        IDamageable.DeadCount.AddListener(Spawn);
    }

    private void OnDisable() { ;
        IDamageable.DeadCount.RemoveListener(Spawn); 
    } 
}