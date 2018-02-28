using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectRoomSprites : MonoBehaviour
{
    public ScriptableRoom   URDL, RDL, UDL, URL,
                            URD, UR, UD, UL, RD,
                            RL, DL, U, R, D, L;

    public bool up, right, down, left;

    public List<Room> deadEnd = new List<Room>();

    public DungeonGeneration rebuild;

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
                        roomData.roomShape = URDL;
                    }
                    else
                    {
                        roomData.roomShape = URD;
                    }
                }
                else if(left)
                {
                        roomData.roomShape = URL;
                }
                else
                {
                    roomData.roomShape = UR;
                }                
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        roomData.roomShape = UDL;
                    }
                    else
                    {
                        roomData.roomShape = UD;
                    }
                }
                else if (left)
                {
                        roomData.roomShape = UL;
                }
                else
                {
                    roomData.roomShape = U;
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
                        roomData.roomShape = RDL;
                    }
                    else
                    {
                        roomData.roomShape = RD;
                    }
                }
                else if (left)
                {
                    roomData.roomShape = RL;
                }
                else
                {
                    roomData.roomShape = R;
                }
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        roomData.roomShape = DL;
                    }
                    else
                    {
                        roomData.roomShape = D;
                    }
                }
                else
                {
                    roomData.roomShape = L;
                }
            }
        }

        // Add all dead ends into an array for special room assignment
        if(roomData.roomShape == U || roomData.roomShape == R || roomData.roomShape == D || roomData.roomShape == L)
        {
             
            deadEnd.Insert(0, roomData);
        }
        else
        {
            // We don't want the start room to be filled with anything
            if (roomData.roomPos == Vector2.zero)
            {
                roomData.roomObject = roomData.roomShape.roomPrefabs[0];
                roomData.FillRoom();
            }
            else
            {
                roomData.roomObject = RandomiseRoomPrefab(roomData);
                roomData.FillRoom();
            }
        }
    }


    // Uses a list of dead ends to assign the 'Special' rooms
    public void AssignSpecialRooms()
    {
        // If there isn't 3 dead ends, recreate the dungeon
        if(deadEnd.Count < 3)
        {
            rebuild.Regenerate();
        }
        else
        {
            int oriSize = deadEnd.Count;

            for (int i = 0; i < oriSize; i++)
            {
                int randRoom = Random.Range(0, deadEnd.Count - 1);

                // Sets room to -0 default
                deadEnd[randRoom].roomObject = deadEnd[randRoom].roomShape.roomPrefabs[0];

                if (i == 0) // Make boss room
                {
                    deadEnd[randRoom].roomType = "boss";
                    deadEnd[randRoom].FillRoom();                    
                }
                else if (i == 1) // Make shop room
                {
                    deadEnd[randRoom].roomType = "shop";
                    deadEnd[randRoom].FillRoom();                    
                }
                else if(i == 2) // Make treasure room
                {
                    deadEnd[randRoom].roomType = "treasure";
                    deadEnd[randRoom].FillRoom();                    
                }
                else // Make normal room
                {
                    deadEnd[randRoom].roomObject = RandomiseRoomPrefab(deadEnd[randRoom]);
                    deadEnd[randRoom].FillRoom();
                }
                
                deadEnd.RemoveAt(randRoom);
            }
        }
    }


    private GameObject RandomiseRoomPrefab(Room roomData)
    {
        int chosen = Random.Range(1, roomData.roomShape.roomPrefabs.Count);
        return roomData.roomShape.roomPrefabs[chosen];
    }
}
