using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.0f;
    public float range = 0.0f;

    private Vector3 startpos;

    // Use this for initialization
    void Start ()
    {        
        startpos = transform.position;
        range = FindObjectOfType<ShootShots>().range;
        speed = FindObjectOfType<ShootShots>().shotSpeed;
        //print("Shot fired. Range: " + range + ". Speed: " + speed);
    }    
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        if(Vector3.Distance(startpos, transform.position) > range)
        {
            Destroy(this.gameObject);
        }
    }
}
