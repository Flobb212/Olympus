using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHit : MonoBehaviour
{
    public GameObject player;    
    public float damage = 1.0f;

    void Start()
    {
        damage = player.GetComponent<PlayerCharacter>().damage;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we're hitting the room
        if(collision.CompareTag("Enemy"))
        {
            collision.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Obstacle") || collision.CompareTag("Room"))
        {
            Destroy(gameObject);
        }
    }
}
