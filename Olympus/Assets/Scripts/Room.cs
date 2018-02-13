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
    public string roomType = "norm";

    //Where the doors are in the room shape
    public bool doorTop, doorBottom, doorLeft, doorRight;

    // Check if player enters the room
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered");
        Camera.main.transform.position = this.transform.position;
    }

    public void fillRoom()
    {
        Vector2 pos = roomPos;
        pos.x *= 18 * roomShape.transform.localScale.x;
        pos.y *= 12* roomShape.transform.localScale.y;

        roomObject = Object.Instantiate(roomShape, pos, roomShape.transform.rotation);
        roomObject.transform.parent = transform.parent;
        gameObject.name = roomObject.name;

        if (roomType == "start")
        {
            roomObject.GetComponent<SpriteRenderer>().color = Color.blue;
            // pull x-0
        }
        else if (roomType == "boss")
        {
            // Activate this line on boss defeat, swap for open trapdoor
            //roomObject.transform.GetChild(0).gameObject.SetActive(true);
            roomObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (roomType == "shop")
        {
            roomObject.GetComponent<SpriteRenderer>().color = Color.green;
            // pull x-0
            // spawn 3 podiums/items with price
        }
        else if (roomType == "treasure")
        {
            roomObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            // pull x-0
            // spawn single podium
        }
        else
        {
            // spawn room from random list
        }

        



        
    }
}
