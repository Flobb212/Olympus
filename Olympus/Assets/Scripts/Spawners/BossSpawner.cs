using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : Spawner
{
    ScriptableRoom shape;    
    public int boss;
    public List<GameObject> bossList;

    public override void Spawn(Room parentRoom)
    {
        //Need to pass room freference to boss somehow in order to seal door
        boss = GetComponentInParent<Room>().bossNum;
        print("spawn boss index of " + boss);
        Instantiate(bossList[boss], this.transform.position, this.transform.rotation);
    }
}
