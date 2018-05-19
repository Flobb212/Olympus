using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaia : MonoBehaviour
{
    public GameObject spawnMarker;
    public GameObject enemySpawner;
    private Vector2 spawnLoc;
    private Room parentRoom;
    private int timer;

    private void Start()
    {
        parentRoom = GetComponent<Boss>().spawnLocation.GetComponent<Room>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        // Pause until spawn
        timer = Random.Range(6, 12);
        yield return new WaitForSeconds(timer);

        // Pick point in room
        spawnLoc = new Vector2( Random.Range(parentRoom.transform.position.x - 6.5f, parentRoom.transform.position.x + 6.5f),
                                Random.Range(parentRoom.transform.position.y - 3.5f, parentRoom.transform.position.y + 3.5f));

        // Place marker and spawner at chosen point
        GameObject marker = Instantiate(spawnMarker, spawnLoc, Quaternion.identity);
        enemySpawner.transform.position = marker.transform.position;
                
        yield return new WaitForSeconds(2);        
        enemySpawner.GetComponent<EnemySpawner>().Spawn(parentRoom.gameObject);
        Destroy(marker);
        
        StartCoroutine(SpawnEnemy());
    }
}

