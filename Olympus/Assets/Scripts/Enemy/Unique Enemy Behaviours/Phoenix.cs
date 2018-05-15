using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoenix : EnemyBehaviour
{
    private int revives = 1;
    public Sprite phoenix;
    public Sprite ashes;

    public override void Die()
    {
        if(revives == 1)
        {
            StartCoroutine(Resurrect());
        }
        else
        {
            spawnLocation.GetComponent<Room>().lockDown.Remove(gameObject);
            FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
            Destroy(gameObject);
        }
    }

    IEnumerator Resurrect()
    {
        revives--;
        health += 5;
        gameObject.GetComponent<SpriteRenderer>().sprite = ashes;

        float tempSpeed = speed;
        speed = 0;
        AdjustSpeed();

        yield return new WaitForSeconds(5);
        gameObject.GetComponent<SpriteRenderer>().sprite = phoenix;
        speed = tempSpeed;
        AdjustSpeed();
    }
}
