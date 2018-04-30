using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damage = 1;
    private bool inDanger = false;
    private Collider2D thePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            thePlayer = collision;
            inDanger = true;
            collision.SendMessage("TakeDamage", damage);
        }
    }

    private void Update()
    {
        if(inDanger == true)
        {
            thePlayer.SendMessage("TakeDamage", damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDanger = false;
        }
    }
}
