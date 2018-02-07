using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    //Chooses which pool of enemies and items to pull from
    //public int floorPool;

    //Dynamically change the size of the floor and how many rooms it has
    public Vector2 floorSize;
    public int numberOfRooms;
    int gridX, gridY;

    //Keep the rooms in an array to easily monitor their position
    //Also keep a list of occupied spaces to avoid building over rooms
    Room[,] rooms;
    List<Vector2> occupied = new List<Vector2>();
    
    public GameObject thisRoom;

    private void Start()
    {
        if(numberOfRooms >= (floorSize.x * 2) * (floorSize.y *2))
        {
            numberOfRooms = Mathf.RoundToInt((floorSize.x * 2) * (floorSize.y * 2));
        }
        gridX = Mathf.RoundToInt(floorSize.x);
        gridY = Mathf.RoundToInt(floorSize.y);
        Generate();
        AttachDoors();
        DrawDungeon();
    }

    void Generate()
    {
        print("Room gen started");

        // Double grid size, may change to half bigger value here later
        rooms = new Room[gridX * 2, gridY * 2];
        // Set start room to center of grid but at 0,0
        rooms[gridX, gridY] = new Room(Vector2.zero, "1");
        // Mark this position as occupied
        occupied.Insert(0, Vector2.zero);
        // Unsure
        Vector2 checkPos = Vector2.zero;

        
        // "Magic numbers"
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        // Add rooms
        for(int i = 0; i < numberOfRooms - 1; i++)
        {
            //Apparently prevents branching as dungeon builds
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            // Grab new position
            checkPos = NewPosition();

            // Helps control branching as dungeon grows
            if (NumberOfNeighbours(checkPos, occupied) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbours(checkPos, occupied) > 1 && iterations < 100);

                if (iterations >= 50)
                {
                    print("error: couldn't create with fewer neighbours than: " + NumberOfNeighbours(checkPos, occupied));
                }
            }

            // Finalise position            
            rooms[(int)checkPos.x + gridX, (int)checkPos.y + gridY] = new Room(checkPos, "1");
            occupied.Insert(0, checkPos);
        }
    }



    // Takes a random placed room, randomly decides which side 
    // to place a new one on, then checks if that's available
    Vector2 NewPosition()
    {
        Vector2 checkingPos = Vector2.zero;
        int x, y =  0;

        do
        {
            int index = Mathf.RoundToInt(Random.value * (occupied.Count - 1));
            x = (int)occupied[index].x;
            y = (int)occupied[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }

            checkingPos = new Vector2(x, y);
        }
        while (occupied.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);

        return checkingPos;
    }



    // Finds room with only one neighbour, to helps restrict branching
    Vector2 SelectiveNewPosition()
    {
        Vector2 checkingPos = Vector2.zero;
        int x = 0, y = 0, index = 0, inc = 0;

        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (occupied.Count - 1));
                inc++;
            }
            while (NumberOfNeighbours(occupied[index], occupied) > 1 && inc < 100);
            
            x = (int)occupied[index].x;
            y = (int)occupied[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }

            checkingPos = new Vector2(x, y);
        }
        while (occupied.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);

        if(inc >= 100)
        {
            print("error: couldn't ind position with just one neighbour");
        }

        return checkingPos;
    }



    // Used to restrict branching when a room has more than one neighbour
    int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;

        if(usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }

        return ret;
    }



    // Checks through the entire room array, checking every point to see if it contains a room
    // If so, check if there is a room in each 4 directions and set that door true/false
    void AttachDoors()
    {
        for(int x = 0; x < (gridX * 2); x++)
        {
            for (int y = 0; y < (gridX * 2); y++)
            {
                // If there's no room at this location, move onto the next
                if(rooms[x, y] == null)
                {
                    continue;
                }

                if(y - 1 < 0) // Then current room is at top of array
                {
                    rooms[x, y].doorBottom = false;
                }
                else // There is space above this room, so check if there is actually a room there
                {
                    rooms[x, y].doorBottom = (rooms[x, y - 1] != null);
                }

                if (y + 1 >= gridY * 2) // Then current room is at bottom of array
                {
                    rooms[x, y].doorTop = false;
                }
                else // There is space below this room, so check if there is actually a room there
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }

                if (x - 1 < 0) // Then current room is at left of array
                {
                    rooms[x, y].doorLeft = false;
                }
                else // There is space left of this room, so check if there is actually a room there
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }

                if (x + 1 >= gridX * 2) // Then current room is at right of array
                {
                    rooms[x, y].doorRight = false;
                }
                else // There is space right of this room, so check if there is actually a room there
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }



    void DrawDungeon()
    {
        foreach(Room room in rooms)
        {
            if (room == null)
            {
                print("Room is null");
                continue;
            }

            print("Going to draw room");
            print(room.roomType);

            Vector2 pos = room.roomPos;
            pos.x *= 6;
            pos.y *= 5;

            SelectRoomSprites builder = Object.Instantiate(thisRoom, pos, Quaternion.identity).GetComponent<SelectRoomSprites>();
            builder.type    = room.roomType;
            builder.up      = room.doorTop;
            builder.right   = room.doorRight;
            builder.down    = room.doorBottom;
            builder.left    = room.doorLeft;
        }
    }
}