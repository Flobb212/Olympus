using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemList items;
    private GameObject spawnedItem;
    private int rand = 0;

    public void Spawn()
    {
        // Would be good to have a chaeck for previously used numbers,
        // However each spawner is unique and this value would need to 
        // Be passed to a global overseer.

        rand = Random.Range(0, items.itemList.Capacity);
        spawnedItem = items.itemList[rand].itemObject;
        Instantiate(spawnedItem, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
    }
}
