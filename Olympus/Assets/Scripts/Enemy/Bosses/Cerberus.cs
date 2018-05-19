using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerberus : MonoBehaviour
{
    public GameObject fireBreath;
    private int timer;

    private void Start()
    {
        StartCoroutine(FireBreath());
    }

    IEnumerator FireBreath()
    {
        // Pause until flames
        timer = Random.Range(8, 10);
        yield return new WaitForSeconds(timer);

        // Stop movement
        AILerp aiLerp = GetComponent<AILerp>();
        if (aiLerp != null)
        {
            aiLerp.enabled = false;
        }
        EnemyWander wander = GetComponent<EnemyWander>();
        if (wander != null)
        {
            wander.enabled = false;
        }

        // Stop shooting
        GetComponent<EnemyShoot>().canShoot = false;

        // Pause to give player chance to move away
        yield return new WaitForSeconds(2);

        // Breath fire in players direction
        fireBreath.transform.up = GetComponent<EnemyShoot>().lookDir;
        GameObject flames = Instantiate(fireBreath, transform.position, fireBreath.transform.rotation);

        // Breath fire for 3 seconds
        yield return new WaitForSeconds(3);

        // Stops fire breath and resumes chasing player
        Destroy(flames);

        if (aiLerp != null)
        {
            aiLerp.enabled = true;
        }
        if (wander != null)
        {
            wander.enabled = true;
        }

        // Start shooting again
        GetComponent<EnemyShoot>().canShoot = true;

        // Start again
        StartCoroutine(FireBreath());
    }
}
