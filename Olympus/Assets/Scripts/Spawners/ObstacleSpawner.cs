using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    public List<GameObject> spawnObj;

    public override void Spawn(Room parentRoom)
    {
        int rand = Random.Range(0, spawnObj.Capacity);

        Instantiate(spawnObj[rand], this.transform.position, this.transform.rotation);
    }
}
