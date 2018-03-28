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
        
        for (var i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == obstacleLayer)
            {
                Collider2D objectCollider = goArray[i].GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(playerCollider, objectCollider);
            }
        }
    }
}
