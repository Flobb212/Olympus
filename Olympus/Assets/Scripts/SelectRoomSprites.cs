using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomSprites : MonoBehaviour
{
    public GameObject   URDL, RDL, UDL, URL,
                    URD, UR, UD, UL, RD,
                    RL, DL, U, R, D, L;

    public bool up, right, down, left;

    public string type;

    GameObject room;

	public void PickRoom(ref Room roomData)
    {
        type = roomData.roomType;
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
                        room = URDL;
                    }
                    else
                    {
                        room = URD;
                    }
                }
                else if(left)
                {
                        room = URL;
                }
                else
                {
                    room = UR;
                }                
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room = UDL;
                    }
                    else
                    {
                        room = UD;
                    }
                }
                else if (left)
                {
                        room = UL;
                }
                else
                {
                    room = U;
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
                        room = RDL;
                    }
                    else
                    {
                        room = RD;
                    }
                }
                else if (left)
                {
                    room = RL;
                }
                else
                {
                    room = R;
                }
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room = DL;
                    }
                    else
                    {
                        room = D;
                    }
                }
                else
                {
                    room = L;
                }
            }            
        }

        Vector2 pos = roomData.roomPos;
        pos.x *= 6 * room.transform.localScale.x;
        pos.y *= 5 * room.transform.localScale.y;
                
        roomData.roomObject = Object.Instantiate(room, pos, room.transform.rotation);

    }

}
