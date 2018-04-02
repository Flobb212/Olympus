using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aegis : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        player.aegis = true;
    }
}
