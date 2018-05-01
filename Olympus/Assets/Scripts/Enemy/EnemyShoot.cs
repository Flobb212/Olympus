using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject shotFired;
    public Transform spawn;
    public float fireRate = 0.7f;
    public float range = 6.0f;
    public float shotSpeed = 5.0f;
    private bool isShooting = false;

    public Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject.transform;
    }

    void Shoot()
    {
        isShooting = true;
        shotFired.GetComponent<Shots>().shooter = spawn.gameObject;
        Instantiate(shotFired, spawn.position, spawn.rotation);
        Invoke("StopShoot", fireRate);
    }

    void StopShoot()
    {
        isShooting = false;
    }

    void Update()
    {
        if(target != null)
        {
            target = FindObjectOfType<PlayerCharacter>().gameObject.transform;

            Vector3 lookDir = target.transform.position - spawn.transform.position;

            Vector3 rotation = Quaternion.LookRotation(lookDir).eulerAngles;
            spawn.transform.up = lookDir;

            Debug.DrawRay(transform.position, lookDir, Color.red);
        }

        if (!isShooting)
        {
            Shoot();
        }
    }

}
