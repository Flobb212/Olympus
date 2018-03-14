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

    public List<GameObject> lockDown;

    // For boss rooms
    public int bossNum = 0;

    void Update()
    {
        print("update boss index of " + bossNum);

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
        if (other.tag != "Player")
        {
            return;
        }

        print("I got a boss index of " + bossNum);

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
            offset.x *= -0.75f;
        }
        else
        {
            // if needed because check is done on player body centre, so feet location are different
            if (offset.y > 0)
            {
                offset.y *= -0.75f;
            }
            else
            {
                offset.y *= -0.55f;
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
                    if(child.tag == "EnemySpawn")
                    {
                        //print("enemy");
                        child.GetComponent<EnemySpawner>().Spawn(this);
                    }
                    else if(child.tag == "BossSpawn")
                    {
                        //print("enemy");
                        child.GetComponent<BossSpawner>().Spawn(this);
                    }
                    else if(child.tag == "ObstacleSpawn")
                    {
                        //print("obstacle");
                        child.GetComponent<ObstacleSpawner>().Spawn();
                    }
                    else if(child.tag == "PickupSpawn")
                    {
                        //print("pickup");
                        child.GetComponent<PickupSpawner>().Spawn();
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
    }
}
