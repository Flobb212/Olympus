using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool canShoot = true;

    public GameObject shotFired;
    public Transform spawn;
    private Transform[] childSpawns;
    public float fireRate = 0.7f;
    public float range = 6.0f;
    public float shotSpeed = 5.0f;
    private bool isShooting = false;
    public bool burstFire = false;
    private bool bursting = false;

    public Transform target;
    public Vector3 lookDir;
    public Vector3 rotation;

    private void Start()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject.transform;
    }

    void Shoot()
    {
        if(canShoot)
        {
            isShooting = true;
            shotFired.GetComponent<Shots>().shooter = spawn.parent.gameObject;

            if(spawn.childCount == 0)
            {
                Instantiate(shotFired, spawn.position, spawn.rotation);
            }
            else
            {
                childSpawns = spawn.GetComponentsInChildren<Transform>();

                foreach(Transform child in childSpawns)
                {
                    if(child.gameObject.name != "Shot Spawn Container")
                    {
                        Instantiate(shotFired, child.position, child.rotation);
                    }    
                }
            }
         
            if(bursting == false)
            {
                Invoke("StopShoot", fireRate);
            }            
        }
    }

    IEnumerator BurstFire()
    {
        bursting = true;

        for (int i = 0; i < 3; i++)
        {
            if (i == 2)
            {
                bursting = false;
            }

            Shoot();
            yield return new WaitForSeconds(0.2f);
        }
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
            lookDir = target.transform.position - spawn.transform.position;
            rotation = Quaternion.LookRotation(lookDir).eulerAngles;
            spawn.transform.up = lookDir;

            Debug.DrawRay(transform.position, lookDir, Color.red);
        }

        if (!isShooting && burstFire == false)
        {
            Shoot();
        }
        else if (!isShooting && burstFire == true)
        {
            StartCoroutine(BurstFire());
        }
    }
}
