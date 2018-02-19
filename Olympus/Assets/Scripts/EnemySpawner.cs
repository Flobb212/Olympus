using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> floor1Enemies;
    public List<GameObject> floor2Enemies;
    public List<GameObject> floor3Enemies;

    // Get floor number from a game manager
    private int floorNum = 0;

    public void Spawn()
    {
        int rand = 0;

        if (floorNum == 1)
        {
            rand = Random.Range(0, floor1Enemies.Capacity);

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
