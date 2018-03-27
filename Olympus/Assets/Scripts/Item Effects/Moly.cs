using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moly : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        base.Activate(player);

        print("Got Moly");

        player.moly = true;
        player.molyBuff = true;
    }
}
