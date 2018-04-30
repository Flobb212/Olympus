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

    public GameObject target;

    private void Start()
    {
        target = FindObjectOfType<PlayerCharacter>().gameObject;
    }

    void Shoot()
    {
        isShooting = true;
        Instantiate(shotFired, spawn.position, spawn.rotation);
        shotFired.GetComponent<Shots>().shooter = this.gameObject;
        Invoke("StopShoot", fireRate);
    }

    void StopShoot()
    {
        isShooting = false;
    }

    void Update()
    {
        Vector3 targetDir = target.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = 3.0f * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        if (!isShooting)
        {
            Shoot();
        }
    }

}
