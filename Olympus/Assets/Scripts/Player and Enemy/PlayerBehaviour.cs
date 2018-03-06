using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int health = 6;
    
    
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		// Press arrow keys to fire
	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Also needs Game Over scenario
            Destroy(this.gameObject);
        }
    }
}
