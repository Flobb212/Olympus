using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Assigns the location of the room
    public Vector2 roomPos;

    //Defines the shape of room and it's contents
    public string roomType;

    //Where the doors are in the room shape
    public bool doorTop, doorBottom, doorLeft, doorRight;

    //Constructor to place correct room in right place
    public Room(Vector2 pos, string type)
    {
        roomPos = pos;
        roomType = type;
    }	
}
