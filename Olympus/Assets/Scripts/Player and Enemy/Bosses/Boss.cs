using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{    
    public float health = 5;
    public Room mySpawn;
    public float speed = 0.0f;

    void Start()
    {
        if (mySpawn != null)
        {
            mySpawn.lockDown.Add(gameObject);
        }
    }

    void Update()
    {

    }

    public void AdjustSpeed()
    {
        this.gameObject.GetComponent<Pathfinding.AILerp>().SettingSpeed(speed);
    }

    public void TakeDamage(ShotHit shot)
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
            health -= shot.damage;
        }
        else if (shot.thisShot == ShotHit.ShotType.Betray)
        {
            print("betray hit");
            //enemy attacks other enemies
        }
        else if (shot.thisShot == ShotHit.ShotType.Death)
        {
            health = 0;
        }

        if (health <= 0)
        {
            mySpawn.lockDown.Remove(gameObject);
            FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
            Destroy(gameObject);
        }
    }

    // Deals damage over time from fire and poison, will pass in diff effects later
    IEnumerator DoT(float damage)
    {
        for (int i = 0; i <= 3; i++)
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
}
