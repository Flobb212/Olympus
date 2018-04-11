using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IBoss
{    
    public float health = 5;
    public GameObject mySpawn;
    public float speed = 0.0f;
    private GameObject endStuff;

    private void Start()
    {
        mySpawn.GetComponent<Room>().lockDown.Add(gameObject);
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
            mySpawn.GetComponent<Room>().lockDown.Remove(gameObject);
            FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
            mySpawn.GetComponent<Room>().endStuff.SetActive(true);
            mySpawn.GetComponent<Room>().endStuff.transform.GetChild(0).GetComponent<ItemSpawner>().Spawn(mySpawn);
            Destroy(gameObject);
        }
    }

    public IEnumerator DoT(float damage)
    {
        for (int i = 0; i <= 3; i++)
        {
            health -= damage;
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator Slowed()
    {
        speed /= 2;
        AdjustSpeed();
        yield return new WaitForSeconds(5);
        speed *= 2;
        AdjustSpeed();
    }
}
