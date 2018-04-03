using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades : MonoBehaviour
{
    public int health = 5;
    public Room mySpawn;

    void Start()
    {
        GetRoom();
    }

    void GetRoom()
    {
        do
        {
            mySpawn = GetComponent<Boss>().spawnLocation;
            print("Waiting for room");
            if (GetComponent<Boss>().spawnLocation != null)
            {
                mySpawn.lockDown.Add(gameObject);
            }
        }
        while (GetComponent<Boss>().spawnLocation == null);
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
