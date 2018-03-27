using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedSandals : PassiveItemEffect
{
    public List<GameObject> exceptions;
    public string obstacleLayer;
    
    public override void Activate(PlayerCharacter player)
    {        
        base.Activate(player);


        Collider2D playerCollider = player.GetComponent<Collider2D>();
        int obstacleLayer = LayerMask.NameToLayer(this.obstacleLayer);

        GameObject[] goArray = FindObjectsOfType<GameObject>();
        List<GameObject> goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == obstacleLayer)
            {
                Collider2D objectCollider = goArray[i].GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(playerCollider, objectCollider);
            }
        }

        //foreach (GameObject X in exceptions)
        //{
        //
        //
        //    Collider2D playerCollider = player.GetComponent<Collider2D>();
        //    Collider2D objectCollider = X.GetComponent<Collider2D>();
        //    Physics2D.IgnoreCollision(playerCollider, objectCollider);
        //    bool ignore = Physics2D.GetIgnoreCollision(playerCollider, objectCollider);
        //    print(X.name + " is ignored " + ignore);
        //    return;
        //}
    }
}
