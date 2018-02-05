using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    //Chooses which pool of enemies and items to pull from
    //public int floorPool;

    //Dynamically change the size of the floor and how many rooms it has
    public Vector2 floorSize;
    int gridX, gridY, numberOfRooms = 20;
    
    //Keep the rooms in an array to easily monitor their position
    //Also keep a list of occupied spaces to avoid building over rooms
    Room[,] rooms;
    List<Vector2> occupied = new List<Vector2>();
    
    //public GameObject thisRoom;

    private void Start()
    {
        gridX = Mathf.RoundToInt(floorSize.x);
        gridY = Mathf.RoundToInt(floorSize.y);
        Generate();
    }

    void Generate()
    {
        // Double grid size, may change to half bigger value here later
        rooms = new Room[gridX * 2, gridY * 2];
        // Set start room to center of grid but at 0,0
        rooms[gridX, gridY] = new Room(Vector2.zero, "1");
        // Mark this position as occupied
        occupied.Insert(0, Vector2.zero);
        // Unsure
        Vector2 checkPos = Vector2.zero;





        // Code at 4:00, writing first then learning later
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

            // Test new position
            if(NumberOfNeighbours(checkPos, occupied) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbours(checkPos, occupied) > 1 && iterations < 100);
            }

            // Finalise position
            rooms[(int)checkPos.x + gridX, (int)checkPos.y + gridY] = new Room(checkPos, "1");
        }
    }

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
        while (occupied.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < gridY);

        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        Vector2 checkingPos = Vector2.zero;
        int x, y, index, inc = 0;

        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (occupied.Count - 1));
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
        while (occupied.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < gridY);

        return checkingPos;
    }

    int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;

        // 6:30

        return ret;
    }



}
