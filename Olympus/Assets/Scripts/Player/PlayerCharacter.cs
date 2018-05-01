using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{    
    public int maxHealth = 6;
    public int currenthealth = 4;
    public float speed = 1.0f;
    public float damage = 1.0f;
    public bool iFrames = false;
    public bool freeze;

    public int coins = 0;
    public Text coinValue;
    public int bombs = 0;
    public Text bombValue;
    public int keys = 0;
    public Text keyValue;

    public Room lastRoom;

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

    // Active item related variables
    public GameObject activeItem;
    public bool hasActive = false;

    public int Coins
    {
        get{return coins;}

        set
        {
            coins = value;
            if(coins > 99)
            {
                coins = 99;
            }
            coinValue = GameObject.Find("CoinValue").GetComponent<Text>();
            coinValue.text = "" + coins;

            if(scalesOfJustice == true)
            {
                ScalesEffect();
            }
        }
    }

    public int Bombs
    {
        get{return bombs;}

        set
        {
            bombs = value;
            if (bombs > 99)
            {
                bombs = 99;
            }
            bombValue = GameObject.Find("BombValue").GetComponent<Text>();
            bombValue.text = "" + bombs;

            if (scalesOfJustice == true)
            {
                ScalesEffect();
            }
        }
    }

    public int Keys
    {
        get{return keys;}

        set
        {
            keys = value;
            if (keys > 99)
            {
                keys = 99;
            }
            keyValue = GameObject.Find("KeyValue").GetComponent<Text>();
            keyValue.text = "" + keys;

            if (scalesOfJustice == true)
            {
                ScalesEffect();
            }
        }
    }


    void FixedUpdate()
    {
        if(freeze == false)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
            GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
        }
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
                return;
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
            scalesBuff = true;
            damage++;
            speed++;
            GetComponent<ShootShots>().range++;
            GetComponent<ShootShots>().shotSpeed++;
            GetComponent<ShootShots>().fireRate += -0.1f;
        }
        else if(scalesBuff == true && (coins != bombs || bombs != keys))
        {
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