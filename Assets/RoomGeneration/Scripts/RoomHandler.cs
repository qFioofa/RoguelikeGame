using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomGeneratorWrapper;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;

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
    [SerializeField] public int numberOfEnemiesTypeA = 1; 
    [SerializeField] public int numberOfEnemiesTypeB = 1;
    private Transform[] spawnPoints; 
    private HashSet<int> occupiedSpawnPoints;
    private int totalEnemies = 0;

    [Header("Sounds")]
    [SerializeField] private AudioClip startOfLvlSound; 
    [SerializeField] private AudioClip lvlFinished;
    public static UnityEvent<int> RoomCleaned = new UnityEvent<int>();

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
        Seed = int.Parse(FullSettingsScript.seed);
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
            ++totalEnemies;
        }
    }

    public void IsRoomCleaned(int deadCount){
        if(!IsCleanedRoomCheck(deadCount)) return;

        totalEnemies = 0;
        isRoomCleaned = true;
        RoomCleaned.Invoke(1);
        SoundFXManager.PlaySoundClipForcePlayer(lvlFinished);
    }
    private bool IsCleanedRoomCheck(int deadCount){
        totalEnemies -= deadCount;
        return totalEnemies<=0;
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
