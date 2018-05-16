using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCharacter : MonoBehaviour
{    
    public int maxHealth = 6;
    public int currenthealth = 4;
    public Image[] heartArray;
    public Sprite heart;
    public Sprite emptyHeart;
    private GameObject gameOverImage;

    public float speed = 1.0f;
    public float damage = 1.0f;
    private bool iFrames = false;
    public bool freeze;
    public bool isSlowed = false;

    public int coins = 0;
    public Text coinValue;
    public int bombs = 0;
    public Text bombValue;
    public int keys = 0;
    public Text keyValue;

    public Room lastRoom;
    public Room currentRoom;

    // Passive item related variables
    public bool scalesOfJustice = false;
    public bool scalesBuff = false;
    public bool moly = false;
    public bool molyBuff = false;
    public bool rodOfAsclepius = false;
    public int kills = 0;
    public bool ambrosia = false;
    public bool respawning = false;
    public bool necklaceOfHarmonia = false;
    public bool aegis = false;

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

    public int CurrentHealth
    {
        get {return currenthealth;}

        set
        {
            currenthealth = value;

            if(currenthealth > maxHealth)
            {
                currenthealth = maxHealth;
            }

            for(int i = 0; i < heartArray.Length; i++)
            {
                if(i < currenthealth)
                {
                    heartArray[i].sprite = heart;
                }
                else
                {
                    heartArray[i].sprite = emptyHeart;
                }

                if (i < maxHealth)
                {
                    heartArray[i].enabled = true;
                }
                else
                {
                    heartArray[i].enabled = false;
                }
            }
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }

        set
        {
            maxHealth = value;
            if(maxHealth > 12)
            {
                maxHealth = 12;
            }
        }
    }

    private void Start()
    {
        gameOverImage = GameObject.Find("Game Over");
        gameOverImage.SetActive(false);

        Image[] heartList = GameObject.FindGameObjectsWithTag("UIHeart").Select((heart) => heart.GetComponent<Image>()).ToArray();
        for(int i = 0; i < heartList.Length; i++)
        {
            heartArray[i] = heartList[i];
        }

        // Trigger heart UI to draw
        CurrentHealth = currenthealth;
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
    

    public void TriggerSlow()
    {
        StartCoroutine(Slowed());
    }

    IEnumerator Slowed()
    {
        if (isSlowed == false)
        {
            isSlowed = true;
            speed /= 2;
            yield return new WaitForSeconds(3.0f);
            speed *= 2;                
            isSlowed = false;
        }
    }


    public void TakeDamage(int damage)
    {
        if(aegis)
        {
            if(Random.Range(0, 10) < 3)
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
            CurrentHealth -= damage;

            if (currenthealth <= 0)
            {
                if(ambrosia == true)
                {
                    print("respawn at " + lastRoom);
                    respawning = true;
                    transform.position = currentRoom.roomPos;
                    transform.position = lastRoom.roomPos;
                    //transform.position = new Vector3(0,0,0);
                    Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);                    
                    respawning = false;
                }
                else
                {
                    // Also needs Game Over scenario
                    gameOverImage.SetActive(true);

                    Destroy(this.gameObject);
                }
            }

            StartCoroutine("Invincible");
        }
    }

    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(2);
        iFrames = false;
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