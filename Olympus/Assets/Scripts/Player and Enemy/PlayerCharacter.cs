using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{    
    public int maxHealth = 6;
    public int currenthealth = 4;
    public float speed = 1.0f;
    public float damage = 1.0f;
    public bool iFrames = false;

    public int coins = 0;
    public int bombs = 0;
    public int keys = 0;
    
    public Room lastRoom;

    // Active item related variables
    public GameObject activeItem;
    public bool hasActive = false;

    // Passive item related variables
    public bool scalesOfJustice = false;
    private bool scalesBuff = false;
    public bool moly = false;
    public bool molyBuff = false;
    public bool rodOfAsclepius = false;
    public int kills = 0;
    public bool ambrosia = false;
    public bool respawning = false;
    public bool necklaceOfHarmonia = false;
    public bool aegis = false;
    public int blockChance = 0;
    
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

            if(scalesOfJustice == true)
            {
                ScalesEffect();
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

            if (scalesOfJustice == true)
            {
                ScalesEffect();
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

            if (scalesOfJustice == true)
            {
                ScalesEffect();
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

    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(2);
        iFrames = false;
    }


    public void TakeDamage(int damage)
    {
        if(aegis == true)
        {
            blockChance = Random.Range(0, 10);

            if(blockChance < 3)
            {
                print("blocked with a " + blockChance);
                return;
            }
            else
            {
                print("hit with a " + blockChance);
            }
        }

        if (molyBuff == true)
        {
            molyBuff = false;
            StartCoroutine("Invincible");
            iFrames = true;
            return;
        }

        if (iFrames == false)
        {
            iFrames = true;
            currenthealth -= damage;

            if (currenthealth <= 0)
            {
                if(ambrosia == true)
                {
                    print("respawn at " + lastRoom);
                    respawning = true;
                    //transform.position = lastRoomPos;
                    transform.position = new Vector3(0,0,0);
                    Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);                    
                    respawning = false;
                }
                else
                {
                    // Also needs Game Over scenario
                    Destroy(this.gameObject);
                }
            }

            StartCoroutine("Invincible");
        }
    }


    public void ScalesEffect()
    {
        if(coins == bombs && bombs == keys && scalesBuff == false)
        {
            //print("balanced");
            scalesBuff = true;
            damage++;
            speed++;
            GetComponent<ShootShots>().range++;
            GetComponent<ShootShots>().shotSpeed++;
            GetComponent<ShootShots>().fireRate += -0.1f;
        }
        else if(scalesBuff == true && (coins != bombs || bombs != keys))
        {
            //print("unbalanced");
            scalesBuff = false;
            damage--;
            speed--;
            GetComponent<ShootShots>().range--;
            GetComponent<ShootShots>().shotSpeed--;
            GetComponent<ShootShots>().fireRate += 0.1f;
        }
    }

    public void AsclepiusEffect()
    {
        if (rodOfAsclepius == true)
        {
            kills++;
            if(kills >= 15)
            {
                currenthealth++;
                kills = 0;
            }
        }
    }

}