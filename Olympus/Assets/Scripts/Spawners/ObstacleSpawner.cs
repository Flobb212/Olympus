using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    public List<GameObject> spawnObj;

    public override void Spawn(GameObject parentRoom)
    {
        int rand = Random.Range(0, spawnObj.Capacity);

        GameObject tObj = Instantiate(spawnObj[rand], this.transform.position, this.transform.rotation);

        //FindObjectOfType<DungeonGeneration>().spawnedThings.Add(tObj);
    }
}
