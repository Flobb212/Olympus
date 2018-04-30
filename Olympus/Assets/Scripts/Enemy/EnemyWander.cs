using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float speed = 0.0f;
    private Vector3 destination;
    private Room parentRoom;
    

    // Use this for initialization
    void Start ()
    {
        if(GetComponent<EnemyBehaviour>().spawnLocation.GetComponent<Room>() != null)
        {
            parentRoom = GetComponent<EnemyBehaviour>().spawnLocation.GetComponent<Room>();
        }
        else
        {
            print("timing issues");
        }
        
        PickDestination();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * 0.01f);

        if((transform.position - destination).magnitude < 1.5)
        {
            StopCoroutine("Countdown");
            PickDestination();
        }
    }

    void PickDestination()
    {
        destination = new Vector3(  Random.Range(parentRoom.transform.position.x - 6.5f, parentRoom.transform.position.x + 6.5f),
                                    Random.Range(parentRoom.transform.position.y - 3.5f, parentRoom.transform.position.y + 3.5f), 0);

        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4);
        PickDestination();
    }
}
