using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveItemEffect : MonoBehaviour, IPassiveBuff
{
    public int health;
    public float damage;
    public float speed;
    public float range;
    public float shotSpeed;
    public float fireRate;

    public virtual void Activate(PlayerCharacter player)
    {
        if (player == null)
        {
            return;
        }

        player.MaxHealth += health;
        player.CurrentHealth += health;
        player.damage += damage;
        player.speed += speed;
        player.GetComponent<ShootShots>().range += range;
        player.GetComponent<ShootShots>().shotSpeed += shotSpeed;
        player.GetComponent<ShootShots>().fireRate += fireRate;
    }
}
