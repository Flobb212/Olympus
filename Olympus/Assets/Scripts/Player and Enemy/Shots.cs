using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour
{
    public float speed = 3.0f;
    public float destroyTimer = 2.0f;

    // Use this for initialization
    void Start ()
    {
        Invoke("Disappear", destroyTimer);
	}

    void Disappear()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}
