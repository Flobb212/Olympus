using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public GameObject shooter;
    public float speed = 5.0f;
    public float range = 6.0f;

    private Vector3 startpos;

    // Use this for initialization
    void Start ()
    {        
        startpos = transform.position;

        if(shooter.tag == "Player")
        {
            range = FindObjectOfType<ShootShots>().range;
            speed = FindObjectOfType<ShootShots>().shotSpeed;
        }
        else if (shooter.transform.tag == "Enemy")
        {
            range = FindObjectOfType<EnemyShoot>().range;
            speed = FindObjectOfType<EnemyShoot>().shotSpeed;
        }
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
