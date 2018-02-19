using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> spawnObj;

    public void Spawn()
    {
        int rand = Random.Range(0, spawnObj.Capacity);

        Instantiate(spawnObj[rand], this.transform.position, this.transform.rotation);
    }

}
