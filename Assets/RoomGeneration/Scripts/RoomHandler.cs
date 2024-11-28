using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomGeneratorWrapper;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.PlayerLoop;

public class RoomHandler : MonoBehaviour{
    private RoomGenerator roomGenerator = new RoomGenerator();
    [Range(0,int.MaxValue)] public int Seed = 0;
    private GameObject RoomWrapper;
    [SerializeField] private GameObject currentRoom;


    void Start(){
        setWrapper();
        roomGenerator.Setup(Seed);
    }

    private float time = 0f;
    bool wait(float sec){
        time += Time.deltaTime;
        if(time>=sec){
            time = 0;
            return true;
        }
        return false;
    }   

    void Update(){
        if(wait(2f)){
            generateNextRoom();
        }
    }
    public void generateNextRoom() { 
        if (currentRoom == null) {
            Debug.LogError("Current Room or Room Generator is not initialized.");
            return;
        }

        Transform currentExit = currentRoom.GetComponent<RoomAttributes>().Exit;
        GameObject newRoom = roomGenerator.GetNextRoom();
        if (newRoom == null) {
            Debug.LogError("Could not generate a new room.");
            return;
        }
        GameObject instantiatedRoom = Instantiate(newRoom, RoomWrapper.transform);
        RoomAttributes roomAttributes = instantiatedRoom.GetComponent<RoomAttributes>();
        roomAttributes.Setup();

        instantiatedRoom.transform.position = currentExit.position;
        instantiatedRoom.transform.rotation = currentExit.rotation;

        instantiatedRoom.transform.position += currentExit.position - roomAttributes.Entry.position;

        currentRoom = instantiatedRoom;
    }

    private void setWrapper(string wrapperTag = "RoomWrapper"){
        RoomWrapper = GameObject.FindGameObjectWithTag(wrapperTag);
    }
}
