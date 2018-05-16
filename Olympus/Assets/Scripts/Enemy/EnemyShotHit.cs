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
            // Check if the gorgon shot the player
            if (GetComponent<Shots>().shooter != null && GetComponent<Shots>().shooter.name == "Gorgon")
            {
                collision.GetComponent<PlayerCharacter>().TriggerSlow();                
            }

            collision.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle") || collision.CompareTag("Room"))
        {
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
