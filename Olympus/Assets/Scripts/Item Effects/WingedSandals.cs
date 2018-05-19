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

        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }
}