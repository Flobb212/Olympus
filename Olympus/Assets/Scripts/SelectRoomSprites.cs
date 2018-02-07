using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoomSprites : MonoBehaviour
{
    public Sprite   URDL, RDL, UDL, URL,
                    URD, UR, UD, UL, RD,
                    RL, DL, U, R, D, L;

    public bool up, right, down, left;

    public string type;

    SpriteRenderer room;

	// Use this for initialization
	void Start ()
    {
        room = GetComponent<SpriteRenderer>();
        PickSprite();
	}

    void PickSprite()
    {
        print("Picking sprite");
        if (up)
        {
            if(right)
            {
                if(down)
                {
                    if(left)
                    {
                        room.sprite = URDL;
                    }
                    else
                    {
                        room.sprite = URD;
                    }
                }
                else if(left)
                {
                        room.sprite = URL;
                }
                else
                {
                    room.sprite = UR;
                }                
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room.sprite = UDL;
                    }
                    else
                    {
                        room.sprite = UD;
                    }
                }
                else if (left)
                {
                        room.sprite = UL;
                }
                else
                {
                    room.sprite = U;
                }                
            }
            return;
        }
        else // No up point
        {
            if (right)
            {
                if (down)
                {
                    if (left)
                    {
                        room.sprite = RDL;
                    }
                    else
                    {
                        room.sprite = RD;
                    }
                }
                else if (left)
                {
                    room.sprite = RL;
                }
                else
                {
                    room.sprite = R;
                }
                return;
            }
            else // No right point
            {
                if (down)
                {
                    if (left)
                    {
                        room.sprite = DL;
                    }
                    else
                    {
                        room.sprite = D;
                    }
                }
                else
                {
                    room.sprite = L;
                }
            }            
        }               
    }
}
