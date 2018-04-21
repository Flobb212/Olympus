using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * 3;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }
}
