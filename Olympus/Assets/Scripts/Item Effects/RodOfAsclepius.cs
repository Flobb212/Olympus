using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodOfAsclepius : PassiveItemEffect
{
    public override void Activate(PlayerCharacter player)
    {
        player.rodOfAsclepius = true;
    }
}
