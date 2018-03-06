using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHit : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we're hitting the room
        if(collision.CompareTag("Enemy"))
        {
            collision.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}
