using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int value = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }

        other.transform.GetComponent<PlayerCharacter>().Coins += value;
        Destroy(this.gameObject);
    }
}
