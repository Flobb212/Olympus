using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helios : MonoBehaviour
{
    public int health = 5;
    public Room mySpawn;

    void Start()
    {
        do
        {
            mySpawn = GetComponent<Boss>().spawnLocation;
            print("Waiting for room");
        }
        while (GetComponent<Boss>().spawnLocation = null);

        mySpawn.lockDown.Add(gameObject);
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            mySpawn.lockDown.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
