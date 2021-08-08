using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public float range;

    public Transform fpsCam;
    // Start is called before the first frame update
    void Start()
    {
        fpsCam = RecursiveFindChild(transform.root, "MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        RaycastHit hit;

        animator.Play("shoot");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            PlayerStats stats = hit.transform.root.GetComponent<PlayerStats>();

            if (hit.collider.transform.tag == "Head") //Headshot
            {
                stats.health -= (2 * this.damage - (this.damage * stats.armor / 100));
                Debug.Log(hit.collider.transform.tag + ": " + stats.health);

                if (stats.health < 0)
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("Enemy killed!");
                }
            }
            else if (hit.collider.transform.tag == "Body") //Bodyshot
            {
                stats.health -= (this.damage - (this.damage * stats.armor / 100));
                Debug.Log(hit.collider.transform.tag + ": " + stats.health);

                if (stats.health < 0)
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("Enemy killed!");
                }
            }
            else if (hit.collider.transform.tag == "Leg") //Legshot
            {
                stats.health -= (0.8f * this.damage - (this.damage * stats.armor / 100));
                Debug.Log(hit.collider.transform.tag + ": " + stats.health);

                if (stats.health < 0)
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("Enemy killed!");
                }
            }
        }

    }
}
