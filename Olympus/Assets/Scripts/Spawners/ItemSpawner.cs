using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : Spawner
{
    public ItemList items;
    private GameObject spawnedItem;
    private int rand = 0;

    private bool itemCollected = false;

    public override void Spawn(GameObject parentRoom)
    {
        // Would be good to have a check for previously used numbers,
        // However each spawner is unique and this value would need to 
        // Be passed to a global overseer.

        rand = Random.Range(0, items.itemList.Capacity);
        spawnedItem = items.itemList[rand].itemObject;
        spawnedItem = Instantiate(spawnedItem, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        FindObjectOfType<DungeonGeneration>().spawnedThings.Add(spawnedItem);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!itemCollected)
        {
            StartCoroutine(DisplayName());
            itemCollected = true;

            FindObjectOfType<DungeonGeneration>().spawnedThings.Remove(spawnedItem);
            spawnedItem.GetComponent<PassiveItemEffect>().Activate(collision.gameObject.GetComponent<PlayerCharacter>());
            Destroy(spawnedItem);
        }
    }

    IEnumerator DisplayName()
    {
        GameObject displayText = GameObject.Find("Name Display");
        string itemName = spawnedItem.name.ToString();
        itemName = itemName.Replace("(Clone)", "");

        displayText.GetComponent<Text>().text = itemName;

        yield return new WaitForSeconds(2);

        displayText.GetComponent<Text>().text = "";
    }
}
