using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : AutomaticWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.damage = 2.5f;
        this.range = 200f;
        this.fireRate = 18f;
        this.nextTimeToFire = 0f;

        fpsCam = RecursiveFindChild(transform.root, "MainCamera");
        animator = transform.root.GetComponent<Animator>();
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
}
