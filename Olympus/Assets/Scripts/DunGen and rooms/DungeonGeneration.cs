using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DungeonGeneration : MonoBehaviour
{
    public LevelTransition screenTransition;
    public bool buildDungeon = true;

    //Dynamically change the size of the floor and how many rooms it has
    public Vector2 floorSize;
    public int numberOfRooms;
    int gridX, gridY;

    //Keep the rooms in an array to easily monitor their position
    //Also keep a list of occupied spaces to avoid building over rooms
    public Room newRoom;
    Room[,] roomsList;
    List<Vector2> occupiedPos = new List<Vector2>();

    public GameObject player;
    private bool playerSpawned = false;
    public int floorNum = 1;
    public SelectRoomPrefab roomCreator;
    public List<GameObject> finalRooms = new List<GameObject>();
    public List<GameObject> spawnedThings = new List<GameObject>();

    int rememberedSeed;
    bool alreadyGenerated = false;

    // Public so if certain conditions aren't met, dungeon can be rebuilt
    public void Start()
    {
        if (buildDungeon)
        {
            // Can produce same seed repeatedly for tests 
            int seed = Random.Range(0, 10000000);

            if (!alreadyGenerated)
            {
                rememberedSeed = seed;
                alreadyGenerated = true;
            }
            else
            {
                rememberedSeed++;
                seed = rememberedSeed;
            }

            Debug.Log(seed);
            Random.InitState(seed);

            if (floorNum == 1)
            {
                GameObject.Instantiate(player);
                playerSpawned = true;
            }
            else
            {
                if (!playerSpawned)
                {
                    playerSpawned = true;
                    GameObject.Instantiate(player);
                }

                player.transform.position = new Vector3(0, 0, 0);
                Camera.main.transform.position = new Vector3(0, 0, -10);
            }

            // Need a minimum size to prevent ridiculous dungeons
            if (numberOfRooms < 15)
            {
                numberOfRooms = 15;
            }

            // Need to prevent more rooms than available in given grid size
            if (numberOfRooms >= (floorSize.x * 2) * (floorSize.y * 2))
            {
                numberOfRooms = Mathf.RoundToInt((floorSize.x * 2) * (floorSize.y * 2));
            }            

            gridX = Mathf.RoundToInt(floorSize.x);
            gridY = Mathf.RoundToInt(floorSize.y);

            Generate();
            AttachDoors();
            BuildDungeon();
        }       
    }


    void Generate()
    {
        // Double grid size to easily keep centre of grid 
        roomsList = new Room[gridX * 2, gridY * 2];
        roomsList[gridX, gridY] = Instantiate(newRoom, newRoom.transform.position, newRoom.transform.rotation);
        roomsList[gridX, gridY].roomPos = Vector2.zero;
        roomsList[gridX, gridY].roomType = "start";

        // Mark this position as occupied
        occupiedPos.Insert(0, Vector2.zero);
        Vector2 chosenPos;

        // Branching limiters
        float limiter = 0.0f;
        float upperBound = 0.2f;
        float lowerBound = 0.01f;

        // Add rooms
        for(int i = 0; i < numberOfRooms - 1; i++)
        {
            // Prevents branching as dungeon builds
            float LerpVal = ((float)i) / (((float)numberOfRooms - 1));
            limiter = Mathf.Lerp(upperBound, lowerBound, LerpVal);           

            // Grab new position
            chosenPos = NewPos();

            // Helps control branching as dungeon grows
            if (SurroundingRooms(chosenPos, occupiedPos) > 1 && Random.value > limiter)
            {
                int j = 0;
                do
                {
                    chosenPos = BranchReductionPos();
                    j++;
                }
                while (SurroundingRooms(chosenPos, occupiedPos) > 1 && j < numberOfRooms * 3);
            }

            // Finalise position            
            roomsList[(int)chosenPos.x + gridX, (int)chosenPos.y + gridY] = Instantiate(newRoom, newRoom.transform.position, newRoom.transform.rotation);
            roomsList[(int)chosenPos.x + gridX, (int)chosenPos.y + gridY].roomPos = chosenPos;
            roomsList[(int)chosenPos.x + gridX, (int)chosenPos.y + gridY].roomType = "norm";
            occupiedPos.Insert(0, chosenPos);
        }
    }


    // Takes a random room, randomly decides which side to
    // place a new one on, then checks if that's available
    Vector2 NewPos()
    {
        Vector2 checkPos = Vector2.zero;
        int x = 0;
        int y = 0;

        do
        {
            int index = Mathf.RoundToInt(Random.value * (occupiedPos.Count - 1));
            x = (int)occupiedPos[index].x;
            y = (int)occupiedPos[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                y += ((positive) ? 1 : -1);
            }
            else
            {
                x += ((positive) ? 1 : -1);
            }

            checkPos = new Vector2(x, y);
        }
        while (occupiedPos.Contains(checkPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);

        return checkPos;
    }


    // Finds room with only one neighbour, to helps restrict branching
    Vector2 BranchReductionPos()
    {
        Vector2 checkingPos = Vector2.zero;
        int x = 0;
        int y = 0;
        int index = 0;
        int inc = 0;

        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (occupiedPos.Count - 1));
                inc++;
            }
            while (SurroundingRooms(occupiedPos[index], occupiedPos) > 1 && inc < 100);
            
            x = (int)occupiedPos[index].x;
            y = (int)occupiedPos[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);

            if (UpDown)
            {
                y += ((positive) ? 1 : -1);
            }
            else
            {
                x += ((positive) ? 1 : -1);
            }

            checkingPos = new Vector2(x, y);
        }
        while (occupiedPos.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);
        
        return checkingPos;
    }


    // Used to restrict branching when a room has more than one neighbour
    int SurroundingRooms(Vector2 checkingPos, List<Vector2> usedPositions)
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
            for (int y = 0; y < (gridY * 2); y++)
            {
                Directions doors = Directions.none;

                // If there's no room at this location, move onto the next
                if(roomsList[x, y] == null)
                {
                    continue;
                }

                // Check if room is above
                if (y + 1 < gridY * 2 && roomsList[x, y + 1] != null)
                {
                    doors = doors | Directions.up;
                }

                // Check if room is right
                if (x + 1 < gridX * 2 && roomsList[x + 1, y] != null)
                {
                    doors = doors | Directions.right;
                }

                // Check if room is below
                if (y - 1 >= 0 && roomsList[x, y - 1] != null)
                {
                    doors = doors | Directions.down;
                }

                // Check if room is left
                if (x - 1 >= 0 && roomsList[x - 1, y] != null)
                {
                    doors = doors | Directions.left;
                }

                roomsList[x, y].doors = doors;
            }
        }
    }
    

    void BuildDungeon()
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
                Destroy(roomsList[x, y].gameObject);
            }
        }        

        roomCreator.AssignSpecialRooms();
    }


    public void Regenerate(bool nextLevel)
    {
        if (nextLevel)
        {
            alreadyGenerated = false;
            floorNum++;
        }

        if (floorNum == 4)
        {
            // Win Condition, can be changed in future iterations
            // Could do things like play a cutscene
            SceneManager.LoadSceneAsync("Victory");
        }
        else
        {
            foreach (GameObject obj in spawnedThings)
            {
                Destroy(obj.gameObject);
            }

            Room[] rooms = FindObjectsOfType<Room>();

            foreach (Room room in rooms)
            {
                Destroy(room.gameObject);
            }

            GetComponent<SelectRoomPrefab>().deadEnd = new List<Room>();
            roomsList = null;
            occupiedPos = new List<Vector2>();
            finalRooms = new List<GameObject>();
            spawnedThings = new List<GameObject>();

            // Needs a bool to check whether the regeneration is because we're going 
            // To the next level or whether the level didn't have 3 dead ends
            if (nextLevel)
            {
                screenTransition.Setup();

                AstarPath obj = FindObjectOfType<AstarPath>();
                obj.data.gridGraph.center = new Vector3(0, 0, 0);
                obj.Scan();
            }
            StartCoroutine("TinyLoadPause");
            
        }
    }

    IEnumerator TinyLoadPause()
    {
        yield return new WaitForSeconds(1);
        this.Start();
    }
}