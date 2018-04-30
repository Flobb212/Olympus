using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotHit : MonoBehaviour
{
    public float damage = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if hit player
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", this);
            Destroy(gameObject);
        }

        // if betrayal is true
        //if (collision.CompareTag("Enemy"))
        //{
        //    collision.SendMessage("TakeDamage", this);
        //    Destroy(gameObject);
        //}
    }
}
