using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShots : MonoBehaviour
{
    public GameObject shotFired;
    public Transform spawn;
    public float fireRate = 0.5f;
    public float range = 6.0f;
    public float shotSpeed = 5.0f;
    private bool isShooting = false;

    public bool fire = false;
    public bool poison = false;
    public bool slow = false;
    public bool fear = false;
    public bool change = false;
    public bool betray = false;
    public bool death = false;

    void Shoot()
    {
        isShooting = true;
        shotFired.GetComponent<Shots>().shooter = this.gameObject;
        Instantiate(shotFired, spawn.position, spawn.rotation);
        shotFired.GetComponent<ShotHit>().fire = fire;
        shotFired.GetComponent<ShotHit>().poison = poison;
        shotFired.GetComponent<ShotHit>().slow = slow;
        shotFired.GetComponent<ShotHit>().fear = fear;
        shotFired.GetComponent<ShotHit>().change = change;
        shotFired.GetComponent<ShotHit>().betray = betray;
        shotFired.GetComponent<ShotHit>().death = death;
        Invoke("StopShoot", fireRate);
    }

    void StopShoot()
    {
        isShooting = false;
    }

    void Update ()
    {
		if(Input.GetKey("up") == true)
        {            
            spawn.rotation = Quaternion.Euler(0, 0, 0);

            if(!isShooting)
            {
                Shoot();
            }
        }
        else if (Input.GetKey("right") == true)
        {
            spawn.rotation = Quaternion.Euler(0, 0, 270);

            if (!isShooting)
            {
                Shoot();
            }
        }
        else if (Input.GetKey("down") == true)
        {
            spawn.rotation = Quaternion.Euler(0, 0, 180);

            if (!isShooting)
            {
                Shoot();
            }
        }
        else if (Input.GetKey("left") == true)
        {
            spawn.rotation = Quaternion.Euler(0, 0, 90);

            if (!isShooting)
            {
                Shoot();
            }
        }
    }   

}
