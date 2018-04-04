 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Room spawnLocation;
    public Sprite changedSprite;
    public float health = 5.0f;
    public enum MoveSpeed { Stationary, Slow, Normal, Fast };
    public MoveSpeed moveType;
    public float speed = 0.0f;

    private bool isChanged = false;

    // Use this for initialization
    void Start ()
    {
        SpeedSelect();
        if(spawnLocation != null)
        {
            spawnLocation.lockDown.Add(gameObject);
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
            speed = 3.5f;
        }
        else if (moveType == MoveSpeed.Fast)
        {
            speed = 5.0f;
        }

        AdjustSpeed();
    }

    public void AdjustSpeed()
    {
        this.gameObject.GetComponent<Pathfinding.AILerp>().SettingSpeed(speed);
    }

    public void TakeDamage(ShotHit shot)
    {
        if(shot.thisShot == ShotHit.ShotType.Normal)
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
            StartCoroutine(Changed());
        }
        else if (shot.thisShot == ShotHit.ShotType.Betray)
        {
            print("betray hit");
            //enemy attacks other enemies
        }
        else if (shot.thisShot == ShotHit.ShotType.Death)
        {
            spawnLocation.lockDown.Remove(gameObject);
            FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
            Destroy(gameObject);
        }                

        if(health <= 0)
        {
            spawnLocation.lockDown.Remove(gameObject);
            FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
            Destroy(gameObject);
        }
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


}
