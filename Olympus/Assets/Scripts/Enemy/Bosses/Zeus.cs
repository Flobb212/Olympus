using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : MonoBehaviour
{
    public GameObject spawnMarker;
    public GameObject bigBolt;
    private Vector2[] spawnLoc = new Vector2[4];
    private Room parentRoom;

    private float maxHealth;
    private float currentHealth;
    private float lightningSpeed = 0f;

    private GameObject[] marker = new GameObject[4];
    private GameObject bolt;

    private void Start()
    {
        maxHealth = GetComponent<Boss>().health;
        parentRoom = GetComponent<Boss>().spawnLocation.GetComponent<Room>();
        StartCoroutine(LightningStrike());
    }

    IEnumerator LightningStrike()
    {
        // Lightning strike speed dependant on health
        currentHealth = GetComponent<Boss>().health;
        lightningSpeed = 2 * (currentHealth / maxHealth);

        if(lightningSpeed < 0.4)
        {
            lightningSpeed = 0.4f;
        }

        // Pick points in room
        for (int i = 0; i < 4; i++)
        {
            spawnLoc[i] = new Vector2(Random.Range(parentRoom.transform.position.x - 6.5f, parentRoom.transform.position.x + 6.5f),
                                    Random.Range(parentRoom.transform.position.y - 3.5f, parentRoom.transform.position.y + 3.5f));
        }
        
        // Pause until spawn
        yield return new WaitForSeconds(lightningSpeed);

        // Place markers at chosen points
        for (int i = 0; i < 4; i++)
        {            
            marker[i] = Instantiate(spawnMarker, spawnLoc[i], Quaternion.identity);
        }        
        
        yield return new WaitForSeconds(lightningSpeed);

        // Strike points
        for (int i = 0; i < 4; i++)
        {
            bolt = Instantiate(bigBolt, spawnLoc[i], Quaternion.identity);
            Destroy(marker[i], 0.5f);
            Destroy(bolt, 0.5f);
        }

        StartCoroutine(LightningStrike());
    }

    void ShatterBolt()
    {
        // Throws bolt at player
        // Explodes at old target position
        // Shoots 4/8 smaller normal bolts out
    }
}
