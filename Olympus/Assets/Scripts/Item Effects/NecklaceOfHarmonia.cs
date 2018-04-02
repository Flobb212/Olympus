using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecklaceOfHarmonia : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        base.Activate(player);

        player.necklaceOfHarmonia = true;
    }
}
