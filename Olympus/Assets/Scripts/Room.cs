using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Attached prefab
    public GameObject roomObject;
    public ScriptableRoom roomShape;
    //Assigns the location of the room
    public Vector2 roomPos;    

    // Defines the shape of room and it's contents
    // Can be: start, norm, boss, treasure or shop
    public string roomType = "norm";

    //Where the doors are in the room shape
    public bool doorTop, doorBottom, doorLeft, doorRight;

    private bool isPopulated = false;
    public GameObject spawners;

    // Check if player enters the room
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter tempPlay = other.GetComponent<PlayerCharacter>();

        if (tempPlay.curRoomPos == null)
        {
            tempPlay.curRoomPos = this.transform;
            return;
        }

        Vector2 oldRoomPos = tempPlay.curRoomPos.position;

        tempPlay.transform.parent = tempPlay.curRoomPos;
        Vector2 offset = other.transform.localPosition;
        tempPlay.transform.parent = null;

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            offset.x *= -1f;
            offset.x *= 0.75f;
        }
        else
        {
            offset.y *= -1f;

            // if needed because check is done on player body centre, so feet location are different
            if (offset.y > 0)
            {
                offset.y *= 0.75f;
            }
            else
            {
                offset.y *= 0.55f;
            }
        }

        tempPlay.curRoomPos = this.transform;
        tempPlay.transform.parent = this.transform;
        tempPlay.transform.localPosition = offset;
        tempPlay.transform.parent = null;

        Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

        // If the room hasn't been entered before, we need to fill it
        if (isPopulated == false)
        {
            // Activate spawners if any
            if (spawners != null)
            {
                foreach (Transform child in spawners.transform)
                {
                    if (child.tag == "EnemySpawn")
                    {
                        print("enemy");
                        child.GetComponent<EnemySpawner>().Spawn();
                    }
                    else if (child.tag == "ObstacleSpawn")
                    {
                        print("obstacle");
                        child.GetComponent<ObstacleSpawner>().Spawn();
                    }
                }
            }            
            
            isPopulated = true;
        }
    }

    public void FillRoom()
    {
        Vector2 pos = roomPos;
        pos.x *= 18;
        pos.y *= 12;

        Object.Instantiate(roomObject, pos, roomObject.transform.rotation);

        // Got bug where prefab trapdoors stay activated even after play has ended

        if (roomType == "boss")
        {
            // Activate this line on boss defeat, swap for open trapdoor
            roomObject.transform.GetChild(0).gameObject.SetActive(true);            
        }
        else if (roomType == "shop")
        {
            // pull x-0
            // spawn 3 podiums/items with price
            roomObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (roomType == "treasure")
        {
            // pull x-0
            // spawn single podium
            roomObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            // spawn room from random list
        }
    }
}
