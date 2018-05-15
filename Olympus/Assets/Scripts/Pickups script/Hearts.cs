using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }

        if(other.transform.GetComponent<PlayerCharacter>().currenthealth < other.transform.GetComponent<PlayerCharacter>().maxHealth)
        {
            other.transform.GetComponent<PlayerCharacter>().CurrentHealth++;
            FindObjectOfType<DungeonGeneration>().spawnedThings.Remove(this.gameObject);
            Destroy(this.gameObject);
        }        
    }
}
