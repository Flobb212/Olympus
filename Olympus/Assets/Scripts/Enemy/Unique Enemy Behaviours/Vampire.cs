using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : EnemyBehaviour
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Vampire attacks!");
            collision.SendMessage("TakeDamage", 1);
        }
    }
}