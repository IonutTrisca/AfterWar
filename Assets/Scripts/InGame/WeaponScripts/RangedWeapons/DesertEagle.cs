using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : SingleShotWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.damage = 3f;
        this.range = 100f;

        fpsCam = RecursiveFindChild(transform.root, "MainCamera");
        animator = transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}
