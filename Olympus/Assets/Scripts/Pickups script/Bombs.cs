using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }

        other.transform.GetComponent<PlayerCharacter>().Bombs++;
        FindObjectOfType<DungeonGeneration>().spawnedThings.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
