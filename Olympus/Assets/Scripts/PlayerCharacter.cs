using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 0.0f;

    public Transform curRoomPos;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Gets input from keyboard to move in axis
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Change the character location based on the speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }
}