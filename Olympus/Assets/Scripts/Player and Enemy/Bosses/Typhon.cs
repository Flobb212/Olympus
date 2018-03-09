using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typhon : MonoBehaviour
{
    public int health = 5;
    public Room spawnLocation;

    void Start()
    {

    }


    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            spawnLocation.lockDown.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}