using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{    
    public int maxHealth = 6;
    public int currenthealth = 4;
    public float speed = 1.0f;
    public float damage = 1.0f;

    public int coins = 0;
    public int bombs = 0;
    public int keys = 0;

    public GameObject activeItem;
    public bool hasActive = false;

    public Transform curRoomPos;
    
    public int Coins
    {
        get
        {
            return coins;
        }

        set
        {
            coins = value;
            if(coins > 99)
            {
                coins = 99;
            }
        }
    }

    public int Bombs
    {
        get
        {
            return bombs;
        }

        set
        {
            bombs = value;
            if (bombs > 99)
            {
                bombs = 99;
            }
        }
    }

    public int Keys
    {
        get
        {
            return keys;
        }

        set
        {
            keys = value;
            if (keys > 99)
            {
                keys = 99;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Gets input from keyboard to move in axis
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Change the character location based on the speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            // Also needs Game Over scenario
            Destroy(this.gameObject);
        }
    }


}