using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadOfAriadne : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        Room[] rooms = FindObjectsOfType<Room>();
        Room bossRoom = null;
        for (int i = 0; i < rooms.Length; i++)
        {
            if(rooms[i].roomType == "boss")
            {
                bossRoom = rooms[i];
                bossRoom.transform.Find("Floor").gameObject.layer = 9;
                bossRoom.transform.Find("Walls").gameObject.layer = 9;
                return;
            }
        }

        if(bossRoom == null)
        {
            throw new System.ArgumentException("No boss room - panic.");
        }
    }
}
