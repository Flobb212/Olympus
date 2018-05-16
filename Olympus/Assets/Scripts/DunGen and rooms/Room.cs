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
    public bool isOccupied = false;

    // Defines the shape of room and it's contents
    // Can be: start, norm, boss, treasure or shop
    public string roomType = "norm";

    public Directions doors = Directions.none;

    private bool isPopulated = false;
    public GameObject spawners;

    public List<GameObject> lockDown;

    // For boss rooms
    public int bossNum = 0;
    public GameObject endStuff;

    void Update()
    {
        if (lockDown.Count != 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    // Check if player enters the room
    void OnTriggerEnter2D(Collider2D other)
    {
        // Entering another trigger causes this to trigger again
        // Need a check to see when the player is in this room and when they leave
        if (other.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            other.gameObject.GetComponent<PlayerCharacter>().currentRoom = this;
            this.transform.Find("Floor").gameObject.layer = 9;
            this.transform.Find("Walls").gameObject.layer = 9;

            if (isOccupied == true || other.gameObject.GetComponent<PlayerCharacter>().respawning == true)
            {
                return;
            }
            else
            {
                isOccupied = true;
            }
        }

        RoomMoving tempPlay = other.GetComponent<RoomMoving>();
        if(tempPlay == null)
        {
            return;
        }     
        else if (tempPlay.curRoom == null)
        {
            tempPlay.curRoom = this;
            return;
        }        

        Room oldRoom = tempPlay.curRoom;
        other.gameObject.GetComponent<PlayerCharacter>().lastRoom = oldRoom;        

        tempPlay.transform.parent = tempPlay.curRoom.transform;
        Vector2 offset = other.transform.localPosition;
        tempPlay.transform.parent = null;

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {            
            offset.x *= -0.75f;
        }
        else
        {
            // if needed because check is done on player body centre, so feet location are different
            if (offset.y > 0)
            {
                offset.y *= -0.55f;
            }
            else
            {
                offset.y *= -0.75f;
            }
        }

        tempPlay.curRoom = this;
        tempPlay.transform.parent = this.transform;
        tempPlay.transform.localPosition = offset;
        tempPlay.transform.parent = null;

        // Tell the old room the player is no longer in it so it can be entered properly again
        other.gameObject.GetComponent<PlayerCharacter>().lastRoom.isOccupied = false;

        if (other.tag == "Player")
        {
            if(other.GetComponent<PlayerCharacter>().moly == true)
            {
                other.GetComponent<PlayerCharacter>().molyBuff = true;
            }
            
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

            // If the room hasn't been entered before, we need to fill it
            if (isPopulated == false)
            {
                // Activate spawners if any
                if (spawners != null)
                {
                    foreach (Transform child in spawners.transform)
                    {
                        child.GetComponent<Spawner>().Spawn(this.gameObject);
                    }
                }

                isPopulated = true;
                               
                // Recalculate grid to new room position
                AstarPath obj = FindObjectOfType<AstarPath>();
                obj.data.gridGraph.center = this.transform.position;
                obj.Scan();
            }
        }
    }

    public GameObject BuildRoom()
    {
        Vector2 pos = roomPos;
        pos.x *= 18;
        pos.y *= 12;

        GameObject newRoom = Instantiate(roomObject, pos, roomObject.transform.rotation);
        Room room = newRoom.GetComponent<Room>();
        room.bossNum = this.bossNum;
        room.roomType = this.roomType;

        FindObjectOfType<DungeonGeneration>().finalRooms.Add(newRoom);

        return newRoom;
    }
}
