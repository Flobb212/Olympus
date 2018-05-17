using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    public bool detonating = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }

        if(detonating == false)
        {
            other.transform.GetComponent<PlayerCharacter>().Bombs++;
            FindObjectOfType<DungeonGeneration>().spawnedThings.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.5f);
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);        

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);        
        
        foreach (Collider2D col in hits)
        {
            if(col.tag == "Obstacle")
            {
                Destroy(col.gameObject);
            }
            else if (col.tag == "Enemy" || col.tag == "Boss")
            {
                col.SendMessage("BombDamage", 10);
            }
            else if (col.tag == "Player")
            {
                col.SendMessage("TakeDamage", 1);
            }
        }

        // Recalculate grid to new room position
        AstarPath obj = FindObjectOfType<AstarPath>();
        obj.data.gridGraph.center = FindObjectOfType<PlayerCharacter>().currentRoom.transform.position;
        obj.Scan();

        Destroy(this.gameObject);
    }
}
