using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveItemEffect : MonoBehaviour, IPassiveBuff
{
    public int Health;
    public float Damage;
    public float Speed;
    public float Range;
    public float ShotSpeed;
    public float FireRate;

    public virtual void Activate(PlayerCharacter player)
    {
        if (player == null)
        {
            return;
        }

        player.currenthealth += Health;
        player.damage += Damage;
        player.speed += Speed;
        player.GetComponent<ShootShots>().range += Range;
        player.GetComponent<ShootShots>().shotSpeed += ShotSpeed;
        player.GetComponent<ShootShots>().fireRate += FireRate;
    }


}
