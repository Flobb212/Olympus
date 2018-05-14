using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : EnemyBehaviour
{
    private int beforeHit = 0;
    private int afterHit = 0;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            beforeHit = collision.GetComponent<PlayerCharacter>().currenthealth;
            collision.SendMessage("TakeDamage", 1);

            afterHit = collision.GetComponent<PlayerCharacter>().currenthealth;
            if (collision != null && beforeHit != afterHit)
            {
                health++;
            }
        }
    }
}