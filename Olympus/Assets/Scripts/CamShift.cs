using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShift : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered");
        Camera.main.transform.position = this.transform.position;
    }
}
