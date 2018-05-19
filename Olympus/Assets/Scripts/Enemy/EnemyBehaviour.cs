 using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject spawnLocation;
    public Sprite changedSprite;
    public float health = 5.0f;
    public enum MoveSpeed { Stationary, Slow, Normal, Fast };
    public MoveSpeed moveType;
    public float speed = 0.0f;

    private bool isChanged = false;
    public bool isImmune = false;

    // Use this for initialization
    public virtual void Start ()
    {
        SpeedSelect();
        if(spawnLocation != null)
        {
            spawnLocation.GetComponent<Room>().lockDown.Add(gameObject);
        }
	}   


    public void SpeedSelect()
    {
        if (moveType == MoveSpeed.Stationary)
        {
            speed = 0.0f;
        }
        else if (moveType == MoveSpeed.Slow)
        {
            speed = 2.0f;
        }
        else if (moveType == MoveSpeed.Normal)
        {
            speed = 3.0f;
        }
        else if (moveType == MoveSpeed.Fast)
        {
            speed = 4.0f;
        }

        AdjustSpeed();
    }

    public void AdjustSpeed()
    {
        if(this.gameObject.GetComponent<Pathfinding.AILerp>())
        {
            this.gameObject.GetComponent<Pathfinding.AILerp>().SettingSpeed(speed);
        }

        if(this.gameObject.GetComponent<EnemyFlying>())
        {
            this.gameObject.GetComponent<EnemyFlying>().speed = speed;
        }

        if (this.gameObject.GetComponent<EnemyWander>())
        {
            this.gameObject.GetComponent<EnemyWander>().speed = speed;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", 1);
        }
    }

    public void TakeDamage(ShotHit shot)
    {
        if (!isImmune)
        {
            if (shot.thisShot == ShotHit.ShotType.Normal)
            {
                health -= shot.damage;
            }
            else if (shot.thisShot == ShotHit.ShotType.Fire)
            {
                StartCoroutine(DoT(shot.damage));
            }
            else if (shot.thisShot == ShotHit.ShotType.Poison)
            {
                StartCoroutine(DoT(shot.damage));
            }
            else if (shot.thisShot == ShotHit.ShotType.Slow)
            {
                StartCoroutine(Slowed());
            }
            else if (shot.thisShot == ShotHit.ShotType.Fear)
            {
                print("fear hit");
                //enemy runs from player
            }
            else if (shot.thisShot == ShotHit.ShotType.Change)
            {
                if(this.gameObject.tag != "Boss")
                {
                    StartCoroutine(Changed());
                }
            }
            else if (shot.thisShot == ShotHit.ShotType.Betray)
            {
                print("betray hit");
                //enemy attacks other enemies
            }
            else if (shot.thisShot == ShotHit.ShotType.Death)
            {
                if (this.gameObject.tag != "Boss")
                {
                    health = 0;
                }
            }
        }

        if(health <= 0)
        {
            Die();
        }
    }

    public void BombDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        spawnLocation.GetComponent<Room>().lockDown.Remove(gameObject);
        FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
        Destroy(gameObject);
    }

    // Deals damage over time from fire and poison, will pass in diff effects later
    IEnumerator DoT(float damage)
    {
        for(int i = 0; i <= 3; i++)
        {
            health -= damage;
            yield return new WaitForSeconds(1);
        }
    }

    // Halves enemy speed and passes it into the A* system
    IEnumerator Slowed()        
    {
        speed /= 2;
        AdjustSpeed();
        yield return new WaitForSeconds(5);
        speed *= 2;
        AdjustSpeed();
    }

    IEnumerator Changed()
    {
        if(isChanged == false)
        {
            isChanged = true;
            Sprite OG = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = changedSprite;

            float tempSpeed = speed;
            speed = 0;
            AdjustSpeed();

            // Stop attack when attack is programmed

            yield return new WaitForSeconds(5);
            gameObject.GetComponent<SpriteRenderer>().sprite = OG;
            speed = tempSpeed;
            isChanged = false;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
