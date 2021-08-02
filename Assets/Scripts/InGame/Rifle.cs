using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public float damage = 3f;
    public float range = 100f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, -this.transform.right, out hit, range))
        {
            if (hit.transform.tag == "Player")
            {
                PlayerStats stats = hit.transform.GetComponent<PlayerStats>();

                stats.health -= (this.damage - (this.damage * stats.armor / 100));
                Debug.Log(stats.health);
            }
        }

    }
}
