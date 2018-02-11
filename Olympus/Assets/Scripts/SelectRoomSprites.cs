using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectRoomSprites : MonoBehaviour
{
    public GameObject   URDL, RDL, UDL, URL,
                    URD, UR, UD, UL, RD,
                    RL, DL, U, R, D, L;

    public bool up, right, down, left;    

    GameObject room;

    public List<Room> deadEnd = new List<Room>();

    public DungeonGeneration retry;

    public void PickRoom(ref Room roomData)
    {        
        up = roomData.doorTop;
        right = roomData.doorRight;
        down = roomData.doorBottom;
        left = roomData.doorLeft;

        if (up)
        {
            if(right)
            {
                if(down)
                {
                    if(left)
                    {
                        room = URDL;
                    }
                    else
                    {
                        room = URD;
                    }
                }
                else if(left)
                {
                        room = URL;
                }
                else
                {
                    room = UR;
                }                
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room = UDL;
                    }
                    else
                    {
                        room = UD;
                    }
                }
                else if (left)
                {
                        room = UL;
                }
                else
                {
                    room = U;
                }                
            }
        }
        else // No up point
        {
            if (right)
            {
                if (down)
                {
                    if (left)
                    {
                        room = RDL;
                    }
                    else
                    {
                        room = RD;
                    }
                }
                else if (left)
                {
                    room = RL;
                }
                else
                {
                    room = R;
                }
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room = DL;
                    }
                    else
                    {
                        room = D;
                    }
                }
                else
                {
                    room = L;
                }
            }            
        }

        roomData.roomShape = room;

        if(room == U || room == R || room == D || room == L)
        {
            deadEnd.Insert(0, roomData);
        }
        else
        {
            roomData.roomType = "norm";
            roomData.fillRoom();
        }
    }


    // Uses a list of dead ends to assign the 'Special' rooms
    public void AssignSpecialRooms()
    {
        // If there isn't 3 dead ends, recreate the dungeon
        if(deadEnd.Count < 3)
        {
            retry.Regenerate();
        }
        else
        {
            int oriSize = deadEnd.Count;

            for (int i = 0; i < oriSize; i++)
            {
                int randRoom = Random.Range(0, deadEnd.Count - 1);

                if (i == 0) // Make boss room
                {
                    deadEnd[randRoom].roomType = "boss";
                    //deadEnd[randRoom].roomShape.transform.GetChild(0).gameObject.SetActive(true);

                    deadEnd[randRoom].fillRoom();                    
                }
                else if (i == 1)
                {
                    deadEnd[randRoom].fillRoom();                    
                }
                else if(i == 2)
                {
                    deadEnd[randRoom].fillRoom();                    
                }
                else
                {
                    deadEnd[randRoom].fillRoom();
                }

                deadEnd.RemoveAt(randRoom);
            }

        }
    }
}
