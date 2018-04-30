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

    void Shoot()
    {
        isShooting = true;
        shotFired.GetComponent<Shots>().shooter = this.gameObject;
        Instantiate(shotFired, spawn.position, spawn.rotation);
        Invoke("StopShoot", fireRate);
    }

    void StopShoot()
    {
        isShooting = false;
    }

    void Update()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject.transform;
        
        Vector3 lookDir = target.transform.position - this.transform.position;
        Quaternion childRot = Quaternion.LookRotation(Vector3.zero, -lookDir)
                    * Quaternion.AngleAxis(0.0f, Vector3.right);


        GetComponentInChildren<Transform>().rotation = childRot;
        
        Debug.DrawRay(transform.position, target.position, Color.red);
        

        if (!isShooting)
        {
            Shoot();
        }
    }

}
