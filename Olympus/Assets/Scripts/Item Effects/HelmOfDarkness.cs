using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmOfDarkness : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        player.HelmOfDarkness();
    }
}
