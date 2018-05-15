using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : EnemyBehaviour
{
    private Transform target;
    private Vector3 destination;
    private Vector3 lookDir;
    private float zoomPower = 10;
    private float timer = 0.0f;

    private bool isCharging = false;
	
	// Update is called once per frame
	void Update ()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject.transform;
        Vector3 playerColliderPosition = new Vector3(target.transform.position.x, target.transform.position.y - 0.4f, target.transform.position.z);
        lookDir = playerColliderPosition - this.transform.position;
        
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lookDir, Vector3.Distance(playerColliderPosition, transform.position));

        timer += Time.deltaTime;

        if (hit.transform != null)
        {
            if (hit.transform.tag == "Player" && !isCharging && timer >= 5)
            {
                print("Locked on Target");
                destination = target.position;
                StartCoroutine(EnemyCharge());
            }
        }
    }

    IEnumerator EnemyCharge()
    {
        isCharging = true;

        AILerp aiLerp = GetComponent<AILerp>();
        if(aiLerp != null)
        {
            aiLerp.enabled = false;
        }
        EnemyWander wander = GetComponent<EnemyWander>();
        if(wander != null)
        {
            wander.enabled = false;
        }

        yield return new WaitForSeconds(1);

        do
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, zoomPower * 0.1f);
            yield return null;
        }
        while (Vector3.Distance(transform.position, destination) >= 0.1);
        
        yield return new WaitForSeconds(1);

        if (aiLerp != null)
        {
            aiLerp.enabled = true;
        }
        if (wander != null)
        {
            wander.enabled = true;
        }

        isCharging = false;
        timer = 0;
    }
}
