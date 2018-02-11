using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Actual room
    public GameObject roomObject;

    // Attached prefab
    public GameObject roomShape;

    //Assigns the location of the room
    public Vector2 roomPos;

    // Defines the shape of room and it's contents
    // Can be: start, norm, boss, treasure or shop
    public string roomType = "start";
    bool isBoss = false;

    //Where the doors are in the room shape
    public bool doorTop, doorBottom, doorLeft, doorRight;
       

    public void fillRoom()
    {
        Vector2 pos = roomPos;
        pos.x *= 6 * roomShape.transform.localScale.x;
        pos.y *= 5 * roomShape.transform.localScale.y;
        roomObject = Object.Instantiate(roomShape, pos, roomShape.transform.rotation);

        if (roomType == "boss")
        {
            isBoss = true;
            roomObject.transform.GetChild(0).gameObject.SetActive(true);
        }

                
        
        roomObject.transform.parent = transform.parent;        
        gameObject.name = roomObject.name;

        //checked type and then send to method based on that
    }
}
