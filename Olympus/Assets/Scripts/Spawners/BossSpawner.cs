using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : Spawner
{
    public int boss;
    public List<GameObject> bossList;

    public override void Spawn(GameObject parentRoom)
    {
        boss = GetComponentInParent<Room>().bossNum;
        bossList[boss].GetComponent<Boss>().spawnLocation = parentRoom;
        Instantiate(bossList[boss], this.transform.position, this.transform.rotation);
    }
}
