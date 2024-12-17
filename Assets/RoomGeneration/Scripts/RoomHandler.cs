using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomGeneratorWrapper;
using System.Linq;
using UnityEngine.AI;

public class RoomHandler : MonoBehaviour{
    private RoomGenerator roomGenerator = new RoomGenerator();
    [Range(0,int.MaxValue)] public int Seed = 0;
    private GameObject RoomWrapper;
    [SerializeField] private GameObject currentRoom;
    [SerializeField] private GameObject previusRoom;
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private bool IsRoomGenerated = false;
    [SerializeField] private bool RoomCleared;

    [Header("Enemy Generation")]
    [SerializeField] private GameObject enemyTypeA; 
    [SerializeField] private GameObject enemyTypeB; 
    [SerializeField] private int numberOfEnemiesTypeA = 4; 
    [SerializeField] private int numberOfEnemiesTypeB = 3;
    private Transform[] spawnPoints; 
    private HashSet<int> occupiedSpawnPoints;
    private List<GameObject> EnemyHolder = new List<GameObject>();

    [Header("Sounds")]
    [SerializeField] private AudioClip startOfLvlSound; 
    [SerializeField] private AudioClip lvlFinished;

    public bool isRoomGenerated{
        get{ return IsRoomGenerated; }
    }
    public bool isRoomCleaned{
        get { return RoomCleared; }
        set {
            IsRoomGenerated = false; 
            RoomCleared = value; 
        }
    }
    public GameObject CurrentRoom{
        get{ return currentRoom; }
    }
    void Start(){
        isRoomCleaned = true;
        setWrapper();
        roomGenerator.Setup(Seed);
        previusRoom = null;
        currentRoom = null;
    }
    public void generateNextRoom() { 
        RoomAttributes roomAttributes = roomGenerator.GetNextRoom().GetComponent<RoomAttributes>();
        roomAttributes.Setup();
        generateNextRoom(roomAttributes.Exit);
    }

    public void generateNextRoom(Transform exit) { 
        previusRoom = currentRoom;
        currentRoom = nextRoom;

        GameObject newRoom = roomGenerator.GetNextRoom();
        if (newRoom == null) {
            Debug.LogError("Could not generate a new room.");
            return;
        }
        nextRoom = Instantiate(newRoom, RoomWrapper.transform);
        RoomAttributes roomAttributes = nextRoom.GetComponent<RoomAttributes>();
        roomAttributes.Setup();

        NavMeshSurface surface = nextRoom.GetComponent<NavMeshSurface>(); 
        if (surface != null) surface.BuildNavMesh();

        nextRoom.transform.position = exit.position;
        nextRoom.transform.rotation = exit.rotation;

        nextRoom.transform.position += exit.position - roomAttributes.Entry.position;
        IsRoomGenerated = true;
    }

    public void StartLvl(EnemyInfo enemyInfo){
        spawnPoints = nextRoom.transform.Find("SpawnPoints")?.GetComponentsInChildren<Transform>();
        occupiedSpawnPoints = new HashSet<int>();
        GenerateEnemy(enemyInfo);
        SoundFXManager.PlaySoundClipForcePlayer(startOfLvlSound);
    }

    public void GenerateEnemy(EnemyInfo enemyInfo){
        SpawnEnemies(enemyTypeA, numberOfEnemiesTypeA, enemyInfo);
        SpawnEnemies(enemyTypeB, numberOfEnemiesTypeB, enemyInfo);
    }

    void SpawnEnemies(GameObject enemyPrefab, int count, EnemyInfo enemyInfo) {
        for (int i = 0; i < count; i++) {
            if (occupiedSpawnPoints.Count >= spawnPoints.Length - 1) break;

            int randomIndex;
            do {
                randomIndex = Random.Range(1, spawnPoints.Length-1); 
            } while (occupiedSpawnPoints.Contains(randomIndex));

            occupiedSpawnPoints.Add(randomIndex); 
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            enemy.GetComponent<IDamageable>().EnemyInfo = enemyInfo;
            if(enemy.GetComponent<CoverShooter>() != null){
                enemy.GetComponent<CoverShooter>().FindCoverPoints(nextRoom.transform);
            }
            EnemyHolder.Add(enemy);
        }
    }

    public void IsRoomCleaned(int _){
        if(!IsCleanedRoomCheck()) return;

        EnemyHolder.Clear();
        isRoomCleaned = true;
        SoundFXManager.PlaySoundClipForcePlayer(lvlFinished);
    }
    private bool IsCleanedRoomCheck(){
        foreach(GameObject enemy in EnemyHolder){
            if(enemy!= null){
                return false;
            }
        }
        return true;
    }
    public void DeletePrevRoom(){
        Destroy(previusRoom);
    }
    private void setWrapper(string wrapperTag = "RoomWrapper"){
        RoomWrapper = GameObject.FindGameObjectWithTag(wrapperTag);
    }

    private void OnEnable() {
        IDamageable.DeadCount.AddListener(IsRoomCleaned);
    }

    private void OnDisable() { ;
        IDamageable.DeadCount.RemoveListener(IsRoomCleaned); 
    } 
}
