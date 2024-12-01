
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Noise;

namespace RoomGeneratorWrapper{
    public class RoomGenerator {
        private int __seed = 0;
        [SerializeField] private GameObject[] roomsList;

        private PerlinNoise noise;
        private readonly int NoiseCapasity = 100;
        private int roomsListLen = 0;
        private int[,] roomsIndexArray;
        private int pointerIndexX = 0;
        private int pointerIndexY = 0;

        public int Seed {
            get { return __seed; }
            set { SetSeed(value); }
        }
 
        public void Setup(int seed, string roomsPrefabs = "RoomsPrefabs/",string roomTag = "Room"){
            SetSeed(seed);
            SetRoomsList(roomsPrefabs,roomTag);
            noise = new PerlinNoise(NoiseCapasity,NoiseCapasity,__seed);
            GenerateArray();
        }

        public GameObject GetNextRoom() {
            return roomsList[GetRoomIndex()];
        }

        private int GetRoomIndex() {
            int return_v = roomsIndexArray[pointerIndexX,pointerIndexY];
            if(++pointerIndexX>=NoiseCapasity){
                pointerIndexX = 0;
                if(++pointerIndexY>=NoiseCapasity){
                    pointerIndexY = 0;
                }
            }

            return return_v;
        }
        private void SetRandomSeed(int maxSeed = int.MaxValue) {
            __seed = Random.Range(0, maxSeed);
        }

        private void SetSeed(int seed = 0) {
            __seed = Mathf.Abs(seed);
        }

        private void SetRoomsList(string path,string roomTag) {
            List<GameObject> roomPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(path));
            /*
            Debug.Log(roomPrefabs.ToArray().Length);
            List<GameObject> filteredRooms = new List<GameObject>();
            foreach (var room in roomPrefabs) {
                if (room.CompareTag(roomTag)) {
                    filteredRooms.Add(room);
                }
            }
            */
            roomsList = roomPrefabs.ToArray();
            roomsListLen = roomsList.Length;
        }
        private void GenerateArray() {
            roomsIndexArray = new int[NoiseCapasity,NoiseCapasity];
            double[,] map = noise.GetGrid();
            for (int i = 0; i < NoiseCapasity; i++) { 
                for (int j=0; j < NoiseCapasity; j++) {
                    roomsIndexArray[i,j] = Mathf.FloorToInt(Mathf.Lerp(0, roomsListLen-1, (float) map[i,j]));
                }
            }
        }
    }
}