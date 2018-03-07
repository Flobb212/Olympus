using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Get floor number from a game manager
    public int floorNum = 0;

    public List<GameObject> floor1Enemies;
    public List<GameObject> floor2Enemies;
    public List<GameObject> floor3Enemies;
        
    public void Spawn(Room newParent)
    {
        int rand = 0;

        if (floorNum == 1)
        {
            rand = Random.Range(0, floor1Enemies.Capacity);
            floor1Enemies[rand].transform.SetParent(newParent.transform);
            Instantiate(floor1Enemies[rand], this.transform.position, this.transform.rotation);
        }
        else if (floorNum == 2)
        {
            rand = Random.Range(0, floor2Enemies.Capacity);

            Instantiate(floor2Enemies[rand], this.transform.position, this.transform.rotation);
        }
        else if (floorNum == 3)
        {
            rand = Random.Range(0, floor3Enemies.Capacity);

            Instantiate(floor3Enemies[rand], this.transform.position, this.transform.rotation);
        }
    }
}
