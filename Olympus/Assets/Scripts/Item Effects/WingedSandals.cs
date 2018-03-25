using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedSandals : PassiveItemEffect
{
    public List<GameObject> exceptions;

    public override void Activate(PlayerCharacter player)
    {        
        base.Activate(player);

        foreach (GameObject X in exceptions)
        {
            Physics2D.IgnoreCollision(FindObjectOfType<PlayerCharacter>().gameObject.GetComponent<BoxCollider2D>(), X.GetComponent<BoxCollider2D>());
            print(X.name);
        }
    }
}
