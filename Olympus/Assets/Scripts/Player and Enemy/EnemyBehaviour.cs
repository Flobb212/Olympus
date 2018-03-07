 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform player;
    //public ScriptableObject data;
    public Room spawnLocation;

    public enum MoveSpeed { Stationary, Slow, Normal, Fast };
    public MoveSpeed moveType;
    public float speed = 0.0f;

    public int health = 5;

	// Use this for initialization
	void Start ()
    {
		if(GameObject.FindGameObjectWithTag("Player") == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spawnLocation.lockDown.Add(gameObject);
        SpeedSelect();
	}
	
	// Update is called once per frame
	void Update()
    {
         //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 0.01f);
	}

    void SpeedSelect()
    {
        if (moveType == MoveSpeed.Stationary)
        {
            speed = 0.0f;
        }
        else if (moveType == MoveSpeed.Slow)
        {
            speed = 2.5f;
        }
        else if (moveType == MoveSpeed.Normal)
        {
            speed = 5.0f;
        }
        else if (moveType == MoveSpeed.Fast)
        {
            speed = 7.5f;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            spawnLocation.lockDown.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
