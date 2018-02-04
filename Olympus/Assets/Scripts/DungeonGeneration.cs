using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    //Chooses which pool of enemies and items to pull from
    public int floorPool;

    //Dynamically change the size of the floor and how many rooms it has
    public Vector2 floorSize;
    public int numberOfRooms;
    
    //Keep the rooms in an array to easily monitor their position
    //Also keep a list of occupied spaces to avoid building over rooms
    Room[,] rooms;
    List<Vector2> occupied = new List<Vector2>();
    
    //public GameObject thisRoom;

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        // 2:56 in vid
    }


}
