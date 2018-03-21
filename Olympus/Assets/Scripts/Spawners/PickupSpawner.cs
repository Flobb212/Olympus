using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Spawner
{
    public List<GameObject> spawnObj;
    private GameObject chosen;

    public override void Spawn(Room parentRoom)
    {
        int rand = Random.Range(0, 100);

        if(rand < 20)
        {
            //Spawn 1 coin
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
        else if (rand > 59 && rand < 80)
        {
            //Spawn heart
            chosen = spawnObj[3];
        }
        else if (rand > 79 && rand < 90)
        {
            //Spawn 5 coin
            chosen = spawnObj[4];
        }
        else if (rand > 89 && rand < 100)
        {
            //Spawn 10 coin
            chosen = spawnObj[5];
        }

        Instantiate(chosen, this.transform.position, this.transform.rotation);
    }
}
