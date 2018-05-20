using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    private Transform player;
    public float speed = 0.0f;

	void Start ()
    {
		if(GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }
	}

    private void Update()
    {
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 0.001f);
        }
    }
}
