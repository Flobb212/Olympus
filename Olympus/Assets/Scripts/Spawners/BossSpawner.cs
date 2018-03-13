using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    ScriptableRoom shape;    
    public int boss;
    public List<GameObject> bossList;

    public void Spawn(Room newParent)
    {
        //Need to pass room freference to boss somehow in order to seal door
        boss = GetComponentInParent<Room>().bossNum;
        print("spawn boss index of " + boss);
        Instantiate(bossList[boss], this.transform.position, this.transform.rotation);
    }
}
