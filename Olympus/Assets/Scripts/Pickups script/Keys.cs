using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }

        other.transform.GetComponent<PlayerCharacter>().Keys++;
        Destroy(this.gameObject);
    }
}
