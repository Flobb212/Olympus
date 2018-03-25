using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalesOfJustice : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        player.scalesOfJustice = true;
        player.ScalesEffect();
    }
}
