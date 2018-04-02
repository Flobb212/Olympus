using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Spawner
{
    public List<GameObject> spawnObj;
    private GameObject chosen;
    public PlayerCharacter thePlayer;

    private int coin1Limit = 80;
    private int coin5Limit = 90;
    private int coin10Limit = 100;

    public override void Spawn(Room parentRoom)
    {
        int rand = Random.Range(0, 100);
        thePlayer = FindObjectOfType<PlayerCharacter>();

        if(thePlayer.necklaceOfHarmonia == true)
        {
            coin1Limit = 70;
            coin5Limit = 85;
            coin10Limit = 95;
        }

        if(rand < 20)
        {
            //Spawn heart
            chosen = spawnObj[0];            
        }
        else if(rand > 19 && rand < 40)
        {
            //Spawn bomb
            chosen = spawnObj[1];
        }
        else if (rand > 39 && rand < 60)
        {
            //Spawn key
            chosen = spawnObj[2];
        }
        else if (rand > 59 && rand < coin1Limit)
        {
            //Spawn 1 coin
            chosen = spawnObj[3];
        }
        else if (rand > 79 && rand < coin5Limit)
        {
            //Spawn 5 coin
            chosen = spawnObj[4];
        }
        else if (rand > 89 && rand < coin10Limit)
        {
            //Spawn 10 coin
            chosen = spawnObj[5];
        }

        if(chosen != null)
        {
            Instantiate(chosen, this.transform.position, this.transform.rotation);
        }        
    }
}
