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
    [SerializeField] private GameObject previusRoom;
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private bool IsRoomGenerated = false;
    [SerializeField] private bool RoomCleared;
    public bool isRoomGenerated{
        get{ return IsRoomGenerated; }
    }
    public bool isRoomCleared{
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
        isRoomCleared = true;
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

        nextRoom.transform.position = exit.position;
        nextRoom.transform.rotation = exit.rotation;

        nextRoom.transform.position += exit.position - roomAttributes.Entry.position;
        IsRoomGenerated = true;
    }
    public void DeletePrevRoom(){
        Destroy(previusRoom);
    }
    private void setWrapper(string wrapperTag = "RoomWrapper"){
        RoomWrapper = GameObject.FindGameObjectWithTag(wrapperTag);
    }
}
