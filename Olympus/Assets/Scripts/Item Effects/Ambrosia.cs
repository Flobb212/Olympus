using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambrosia : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        player.ambrosia = true;
    }
}
