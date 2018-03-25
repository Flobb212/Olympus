using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moly : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        print("Got Moly");

        base.Activate(player);
        
        player.moly = true;
        player.molyBuff = true;
    }
}
