using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHit : MonoBehaviour
{
    public GameObject player;    
    public float damage = 1.0f;        
    
    public enum ShotType { Normal, Fire, Poison, Slow, Fear, Change, Betray, Death };
    public ShotType thisShot;
    public bool fire = false;
    public bool poison = false;
    public bool slow = false;
    public bool change = false;
    public bool betray = false;
    public bool fear = false;
    public bool death = false;

    void Start()
    {
        ChooseShotType();
        damage = FindObjectOfType<PlayerCharacter>().damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we're hitting the room
        if(collision.CompareTag("Enemy"))
        {
            collision.SendMessage("TakeDamage", this);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Obstacle") || collision.CompareTag("Room"))
        {
            Destroy(gameObject);
        }
    }

    public void ChooseShotType()
    {
        int rand = Random.Range(0, 100);

        if (fire == true && rand < 5)
        {
            print("fire shot");
            thisShot = ShotType.Fire;
        }
        else if (poison == true && rand >= 5 && rand < 10)
        {
            print("poison shot");
            thisShot = ShotType.Poison;
        }
        else if (slow == true && rand >= 10 && rand < 15)
        {
            print("slow shot");
            thisShot = ShotType.Slow;
        }
        else if (fear == true && rand >= 15 && rand < 20)
        {
            print("fear shot");
            thisShot = ShotType.Fear;
        }
        else if (change == true && rand >= 20 && rand < 25)
        {
            print("change shot");
            thisShot = ShotType.Change;
        }
        else if (betray == true && rand >= 25 && rand < 30)
        {
            print("betray shot");
            thisShot = ShotType.Betray;
        }
        else if (death == true && (rand == 31 || rand == 32))
        {
            print("death shot");
            thisShot = ShotType.Death;
        }
        else
        {
            print("normal shot");
            thisShot = ShotType.Normal;
        }
    }
}
