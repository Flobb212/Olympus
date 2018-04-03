using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Room spawnLocation;

    public void GetRoom(Room spawnRoom)
    {
        spawnLocation = spawnRoom;
    }
}
