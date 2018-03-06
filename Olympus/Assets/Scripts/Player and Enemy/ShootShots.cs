using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShots : MonoBehaviour
{
    public GameObject shotType;
    public Transform spawn;
    public float fireRate = 0.5f;
    private bool isShooting = false;
	
	void Shoot()
    {
        isShooting = true;
        Instantiate(shotType, spawn.position, spawn.rotation);
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
