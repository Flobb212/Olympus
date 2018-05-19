using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectRoomPrefab : MonoBehaviour
{
    public ScriptableRoom[] roomPrefabs;

    public ScriptableRoom boss, treasure, shop;
   

    public bool up, right, down, left;
    public List<Room> deadEnd = new List<Room>();
    public DungeonGeneration rebuild;

    public int currFloor = 0;    

    public void PickRoom(ref Room roomData)
    {
        // The enum value for the doors works as an index
        roomData.roomShape = roomPrefabs[(int)roomData.doors];
        

        // We don't want the start room to be filled with anything
        if (roomData.roomPos == Vector2.zero)
        {
            roomData.roomObject = roomData.roomShape.roomPrefabs[0];
            roomData.BuildRoom();
        }
        // Add all dead ends into an array for special room assignment
        else if (roomData.doors == Directions.up || roomData.doors == Directions.right || roomData.doors == Directions.down || roomData.doors == Directions.left)
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
        // If there isn't 4 dead ends, recreate the dungeon
        // Used to be 3 but if centre room is dead end, there may be problems
        if(deadEnd.Count < 4)
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
        bossRoom.roomShape = boss;
        bossRoom.roomType = "boss";        

        int randBoss = Random.Range(0, 3);
        int bossIndex = 0;

        currFloor = FindObjectOfType<DungeonGeneration>().floorNum;

        // Since certain bosses need certain rooms, the boss must be chosen before room assignment
        bossIndex = randBoss + (3 * (currFloor - 1));

        // Now we have a boss reference, assign the correct shape room
        if (bossIndex == 1) // Charon has been picked
        {
            bossRoom.roomObject = boss.roomPrefabs[(int)Mathf.Log((float)(int)bossRoom.doors, 2)];
        }
        else if(bossIndex == 3) // Posiedon has been picked
        {
            bossRoom.roomObject = boss.roomPrefabs[(int)Mathf.Log((float)(int)bossRoom.doors, 2) + 4];            
        }
        else // Doesn't need a special room
        {
            int startIndex = 8;
            int genericRooms = 4;
            int doorIndex = (int)Mathf.Log((float)(int)bossRoom.doors, 2);
            int index = startIndex + (genericRooms * doorIndex) + Random.Range(0, 3);
                        
            bossRoom.roomObject = boss.roomPrefabs[index];
        }
        
        GameObject newObject = bossRoom.BuildRoom();
        newObject.GetComponent<Room>().bossNum = bossIndex;
    }


    private void HandleTreasureRoom(Room treasureRoom)
    {        
        treasureRoom.roomShape = treasure;
        treasureRoom.roomType = "treasure";

        //Need to make and add door locks on floors >1

        treasureRoom.roomObject = shop.roomPrefabs[(int)Mathf.Log((float)(int)treasureRoom.doors, 2)];

        treasureRoom.BuildRoom();
    }


    private void HandleShopRoom(Room shopRoom)
    {
        shopRoom.roomShape = shop;
        shopRoom.roomType = "shop";
        
        shopRoom.roomObject = shop.roomPrefabs[(int)Mathf.Log((float)(int)shopRoom.doors, 2)];        

        shopRoom.BuildRoom();
    }
}
