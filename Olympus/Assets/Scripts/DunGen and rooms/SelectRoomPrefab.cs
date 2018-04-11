﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectRoomPrefab : MonoBehaviour
{
    public ScriptableRoom   URDL, RDL, UDL, URL,
                            URD, UR, UD, UL, RD,
                            RL, DL, U, R, D, L,
                            boss, treasure, shop;

    public bool up, right, down, left;
    public List<Room> deadEnd = new List<Room>();
    public DungeonGeneration rebuild;

    public int currFloor = 0;    

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

        // We don't want the start room to be filled with anything
        if (roomData.roomPos == Vector2.zero)
        {
            roomData.roomObject = roomData.roomShape.roomPrefabs[0];
            roomData.BuildRoom();
        }
        // Add all dead ends into an array for special room assignment
        else if (roomData.roomShape == U || roomData.roomShape == R || roomData.roomShape == D || roomData.roomShape == L)
        {
            deadEnd.Insert(0, roomData);
        }
        else
        {
            roomData.roomObject = RandomiseRoomPrefab(roomData);
            roomData.BuildRoom();
        }
    }


    // Uses a list of dead ends to assign the 'Special' rooms
    public void AssignSpecialRooms()
    {
        // If there isn't 3 dead ends, recreate the dungeon
        if(deadEnd.Count < 3)
        {
            rebuild.Regenerate(false);
        }
        else
        {
            int roomsLeft = deadEnd.Count;

            for (int i = 0; i < roomsLeft; i++)
            {
                int randRoom = Random.Range(0, deadEnd.Count - 1);

                // Sets room to -0 default
                deadEnd[randRoom].roomObject = deadEnd[randRoom].roomShape.roomPrefabs[0];

                if (i == 0) // Make boss room
                {
                    HandleBossRoom(deadEnd[randRoom]);
                }
                else if (i == 1) // Make shop room
                {
                    HandleShopRoom(deadEnd[randRoom]);
                }
                else if(i == 2) // Make treasure room
                {
                    HandleTreasureRoom(deadEnd[randRoom]);
                }
                else // Make normal room
                {
                    deadEnd[randRoom].roomObject = RandomiseRoomPrefab(deadEnd[randRoom]);
                    deadEnd[randRoom].BuildRoom();
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


    // Confusing setup for handling boss rooms but adding in proved complicated otherwise
    // May shift into Boss spawner script if possible
    private void HandleBossRoom(Room bossRoom)
    {
        ScriptableRoom baseShape = bossRoom.roomShape;
        bossRoom.roomShape = boss;
        bossRoom.roomType = "boss";        

        int randBoss = Random.Range(1, 4);
        int bossIndex = 0;

        currFloor = FindObjectOfType<DungeonGeneration>().floorNum;

        // Since certain bosses need certain rooms, the boss must be chosen before room assignment
        if (currFloor == 1)
        {
            bossIndex = randBoss - currFloor;
        }
        else if(currFloor == 2)
        {
            bossIndex = randBoss + currFloor;
        }
        else
        {
            if(randBoss == 1)
            {
                bossIndex = 6;
            }
            else if(randBoss == 2)
            {
                bossIndex = 7;
            }
            else
            {
                bossIndex = 8;
            }
        }
        
        int randRoom;

        // Now we have a boss reference, assign the correct shape room
        if (bossIndex == 1) // Charon has been picked
        {
            if(baseShape == U)
            {
                bossRoom.roomObject = boss.roomPrefabs[0];
            }
            else if(baseShape == R)
            {
                bossRoom.roomObject = boss.roomPrefabs[1];
            }
            else if (baseShape == D)
            {
                bossRoom.roomObject = boss.roomPrefabs[2];
            }
            else
            {
                bossRoom.roomObject = boss.roomPrefabs[3];
            }
        }
        else if(bossIndex == 3) // Posiedon has been picked
        {
            if (baseShape == U)
            {
                bossRoom.roomObject = boss.roomPrefabs[4];
            }
            else if (baseShape == R)
            {
                bossRoom.roomObject = boss.roomPrefabs[5];
            }
            else if (baseShape == D)
            {
                bossRoom.roomObject = boss.roomPrefabs[6];
            }
            else
            {
                bossRoom.roomObject = boss.roomPrefabs[7];
            }
        }
        else // Doesn't need a special room
        {
            if (baseShape == U)
            {
                randRoom = Random.Range(8, 12);
                bossRoom.roomObject = boss.roomPrefabs[randRoom];
            }
            else if (baseShape == R)
            {
                randRoom = Random.Range(12, 16);
                bossRoom.roomObject = boss.roomPrefabs[randRoom];
            }
            else if (baseShape == D)
            {
                randRoom = Random.Range(16, 20);
                bossRoom.roomObject = boss.roomPrefabs[randRoom];
            }
            else
            {
                randRoom = Random.Range(20, 24);
                bossRoom.roomObject = boss.roomPrefabs[randRoom];
            }
        }
        
        GameObject newObject = bossRoom.BuildRoom();
        newObject.GetComponent<Room>().bossNum = bossIndex;
    }


    private void HandleTreasureRoom(Room treasureRoom)
    {
        ScriptableRoom baseShape = treasureRoom.roomShape;
        treasureRoom.roomShape = treasure;
        treasureRoom.roomType = "treasure";

        //Need to make and add door locks on floors >1

        if(baseShape == U)
        {
            treasureRoom.roomObject = treasure.roomPrefabs[0];
        }
        else if (baseShape == R)
        {
            treasureRoom.roomObject = treasure.roomPrefabs[1];
        }
        else if (baseShape == D)
        {
            treasureRoom.roomObject = treasure.roomPrefabs[2];
        }
        else
        {
            treasureRoom.roomObject = treasure.roomPrefabs[3];
        }

        treasureRoom.BuildRoom();
    }


    private void HandleShopRoom(Room shopRoom)
    {
        ScriptableRoom baseShape = shopRoom.roomShape;
        shopRoom.roomShape = shop;
        shopRoom.roomType = "shop";

        //Need to make and add door locks

        if (baseShape == U)
        {
            shopRoom.roomObject = shop.roomPrefabs[0];
        }
        else if (baseShape == R)
        {
            shopRoom.roomObject = shop.roomPrefabs[1];
        }
        else if (baseShape == D)
        {
            shopRoom.roomObject = shop.roomPrefabs[2];
        }
        else
        {
            shopRoom.roomObject = shop.roomPrefabs[3];
        }

        shopRoom.BuildRoom();
    }
}
