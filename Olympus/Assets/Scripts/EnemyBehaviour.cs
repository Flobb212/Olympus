using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform player;
    public bool isStationary = false;
    public float speed = 5.0f;


	// Use this for initialization
	void Start ()
    {
		if(GameObject.FindGameObjectWithTag("Player") == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        // Only apply seeking if enemy isn't stationary
		if(isStationary == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 0.01f);
        }
	}
}
