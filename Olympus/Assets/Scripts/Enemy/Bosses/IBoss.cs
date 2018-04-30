using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoss
{
    void AdjustSpeed();
    void TakeDamage(ShotHit shot);
    IEnumerator DoT(float damage);
    IEnumerator Slowed();
}
