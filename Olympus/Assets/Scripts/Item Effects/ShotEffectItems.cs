using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffectItems : PassiveItemEffect
{
    public bool fire = false;
    public bool poison = false;
    public bool slow = false;
    public bool fear = false;
    public bool change = false;
    public bool betray = false;
    public bool death = false;

    public override void Activate(PlayerCharacter player)
    {
        if(fire == true)
        {
            player.GetComponent<ShootShots>().fire = true;
        }
        else if(poison == true)
        {
            player.GetComponent<ShootShots>().poison = true;
        }
        else if(slow == true)
        {
            player.GetComponent<ShootShots>().slow = true;
        }
        else if (fear == true)
        {
            player.GetComponent<ShootShots>().fear = true;
        }
        else if (change == true)
        {
            player.GetComponent<ShootShots>().change = true;
        }
        else if (betray == true)
        {
            player.GetComponent<ShootShots>().betray = true;
        }
        else if(death == true)
        {
            player.GetComponent<ShootShots>().death = true;
        }
    }
}
