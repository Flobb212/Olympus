using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sirens : EnemyBehaviour
{
    public int timer = 0;
    public Sprite siren;
    public Sprite underwater;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Dive());
    }

    IEnumerator Dive()
    {
        timer = Random.Range(5, 10);
        yield return new WaitForSeconds(timer);

        isImmune = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = underwater;        
        GetComponent<EnemyShoot>().canShoot = false;

        timer = Random.Range(2, 4);
        yield return new WaitForSeconds(timer);

        isImmune = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = siren;
        GetComponent<EnemyShoot>().canShoot = true;

        StartCoroutine(Dive());
    }

}
