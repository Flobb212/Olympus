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

                GameObject roomParts = bossRoom.transform.Find("Room Bits").gameObject;
                Transform[] childParts = roomParts.GetComponentsInChildren<Transform>();

                foreach (Transform child in childParts)
                {
                    child.gameObject.layer = 9;
                }

                return;
            }
        }

        if(bossRoom == null)
        {
            throw new System.ArgumentException("No boss room - panic.");
        }
    }
}
