 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Room spawnLocation;
    public float health = 5.0f;
    public enum MoveSpeed { Stationary, Slow, Normal, Fast };
    public MoveSpeed moveType;
    public float speed = 0.0f;

    // Use this for initialization
    void Start ()
    {
        SpeedSelect();
        //spawnLocation.lockDown.Add(gameObject);
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
            speed = 4.0f;
        }
        else if (moveType == MoveSpeed.Fast)
        {
            speed = 6.0f;
        }

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
            StartCoroutine(DoT());
        }
        else if (shot.thisShot == ShotHit.ShotType.Poison)
        {
            StartCoroutine(DoT());
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
            print("change hit");
            //enemy becomes motionless and can't attack
        }
        else if (shot.thisShot == ShotHit.ShotType.Betray)
        {
            print("betray hit");
            //enemy attacks other enemies
        }
        else if (shot.thisShot == ShotHit.ShotType.Death)
        {
            print("death hit");
            spawnLocation.lockDown.Remove(gameObject);
            Destroy(gameObject);
        }                

        if(health <= 0)
        {
            //spawnLocation.lockDown.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    // Deals damage over time from fire and poison, will pass in diff effects later
    IEnumerator DoT()
    {
        for(int i = 0; i <= 3; i++)
        {
            health--;
            yield return new WaitForSeconds(1);
        }
    }

    // Halves enemy speed and passes it into the A* system
    IEnumerator Slowed()        
    {
        speed /= 2;
        this.gameObject.GetComponent<Pathfinding.AILerp>().SettingSpeed(speed);
        yield return new WaitForSeconds(5);
        speed *= 2;
        this.gameObject.GetComponent<Pathfinding.AILerp>().SettingSpeed(speed);
    }


}
