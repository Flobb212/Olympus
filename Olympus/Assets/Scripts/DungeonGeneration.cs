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
    public Room newRoom;
    Room[,] roomsList;
    List<Vector2> occupiedPos = new List<Vector2>();

    public GameObject testHolder;

    public GameObject player;    
    public SelectRoomSprites roomCreator;

    //Testing Scriptable Rooms
    //public ScriptableRoom newRoom;
    
    
    // Public so if certain conditions aren't met, dungeon can be rebuilt
    public void Start()
    {
        GameObject.Instantiate(player);
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
        // Double grid size, may change to half bigger value here later
        roomsList = new Room[gridX * 2, gridY * 2];
        roomsList[gridX, gridY] = Instantiate(newRoom, newRoom.transform.position, newRoom.transform.rotation);
        roomsList[gridX, gridY].roomPos = Vector2.zero;
        roomsList[gridX, gridY].roomType = "start";

        // Mark this position as occupied
        occupiedPos.Insert(0, Vector2.zero);
        Vector2 checkPos;

        
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
            if (NumberOfNeighbours(checkPos, occupiedPos) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbours(checkPos, occupiedPos) > 1 && iterations < 100);

                if (iterations >= 50)
                {
                    print("error: couldn't create with fewer neighbours than: " + NumberOfNeighbours(checkPos, occupiedPos));
                }
            }

            // Finalise position            
            roomsList[(int)checkPos.x + gridX, (int)checkPos.y + gridY] = Instantiate(newRoom, newRoom.transform.position, newRoom.transform.rotation);
            roomsList[(int)checkPos.x + gridX, (int)checkPos.y + gridY].roomPos = checkPos;
            roomsList[(int)checkPos.x + gridX, (int)checkPos.y + gridY].roomType = "norm";
            occupiedPos.Insert(0, checkPos);
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
            int index = Mathf.RoundToInt(Random.value * (occupiedPos.Count - 1));
            x = (int)occupiedPos[index].x;
            y = (int)occupiedPos[index].y;
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
        while (occupiedPos.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);

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
                index = Mathf.RoundToInt(Random.value * (occupiedPos.Count - 1));
                inc++;
            }
            while (NumberOfNeighbours(occupiedPos[index], occupiedPos) > 1 && inc < 100);
            
            x = (int)occupiedPos[index].x;
            y = (int)occupiedPos[index].y;
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
        while (occupiedPos.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);
        
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
                if(roomsList[x, y] == null)
                {
                    continue;
                }

                if(y - 1 < 0) // Then current room is at top of array
                {
                    roomsList[x, y].doorBottom = false;
                }
                else // There is space above this room, so check if there is actually a room there
                {
                    roomsList[x, y].doorBottom = (roomsList[x, y - 1] != null);
                }

                if (y + 1 >= gridY * 2) // Then current room is at bottom of array
                {
                    roomsList[x, y].doorTop = false;
                }
                else // There is space below this room, so check if there is actually a room there
                {
                    roomsList[x, y].doorTop = (roomsList[x, y + 1] != null);
                }

                if (x - 1 < 0) // Then current room is at left of array
                {
                    roomsList[x, y].doorLeft = false;
                }
                else // There is space left of this room, so check if there is actually a room there
                {
                    roomsList[x, y].doorLeft = (roomsList[x - 1, y] != null);
                }

                if (x + 1 >= gridX * 2) // Then current room is at right of array
                {
                    roomsList[x, y].doorRight = false;
                }
                else // There is space right of this room, so check if there is actually a room there
                {
                    roomsList[x, y].doorRight = (roomsList[x + 1, y] != null);
                }
            }
        }
    }
    

    void DrawDungeon()
    {
        for (int x = 0; x < gridX * 2; x++)
        {
            for (int y = 0; y < gridY * 2; y++)
            {
                if (roomsList[x, y] == null)
                {
                    continue;
                }
                
                roomCreator.PickRoom(ref roomsList[x, y]);
            }
        }        

        roomCreator.AssignSpecialRooms();
    }


    public void Regenerate()
    {
        for (int x = 0; x < gridX * 2; x++)
        {
            for (int y = 0; y < gridY * 2; y++)
            {
                Destroy(roomsList[x, y]);
                this.Start();
            }
        }
    }
}