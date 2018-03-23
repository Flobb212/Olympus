using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class ItemSpawner : Spawner
{
    public ItemList items;
    private GameObject spawnedItem;
    private int rand = 0;

    private bool itemCollected = false;

    public override void Spawn(Room parentRoom)
    {
        // Would be good to have a check for previously used numbers,
        // However each spawner is unique and this value would need to 
        // Be passed to a global overseer.

        rand = Random.Range(0, items.itemList.Capacity);
        spawnedItem = items.itemList[rand].itemObject;
        spawnedItem = Instantiate(spawnedItem, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!itemCollected)
        {
            itemCollected = true;

            spawnedItem.GetComponent<PassiveItemEffect>().Activate(collision.gameObject.GetComponent<PlayerCharacter>());
            Destroy(spawnedItem);
        }
    }
}
