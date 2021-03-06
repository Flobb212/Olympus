﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kobalos : EnemyBehaviour
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", 1);
            if(collision.GetComponent<PlayerCharacter>().coins != 0)
            {
                collision.GetComponent<PlayerCharacter>().Coins--;
            }
        }
    }
}
