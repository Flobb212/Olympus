using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBehaviour
{
    public override void Start()
    {
        base.Start();
        StartCoroutine(DisplayName());
    }

    IEnumerator DisplayName()
    {
        GameObject displayText = GameObject.Find("Name Display");
        string bossName = this.name.ToString();
        bossName = bossName.Remove(0, 3);
        bossName = bossName.Replace("(Clone)", "");

        displayText.GetComponent<Text>().text = bossName;

        yield return new WaitForSeconds(2);

        displayText.GetComponent<Text>().text = "";
    }

    public override void Die()
    {
        spawnLocation.GetComponent<Room>().lockDown.Remove(gameObject);
        FindObjectOfType<PlayerCharacter>().AsclepiusEffect();
        spawnLocation.GetComponent<Room>().endStuff.SetActive(true);
        spawnLocation.GetComponent<Room>().endStuff.transform.GetChild(0).GetComponent<ItemSpawner>().Spawn(spawnLocation);
        Destroy(gameObject);
    }
}