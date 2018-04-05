using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private bool restarted = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(restarted == false)
        {
            if (collision.tag == "Player")
            {
                restarted = true;
                FindObjectOfType<DungeonGeneration>().player = collision.gameObject;
                FindObjectOfType<DungeonGeneration>().Regenerate();
            }
        }

        
    }
}
