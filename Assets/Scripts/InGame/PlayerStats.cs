using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public int magazine;
    public int reserve;

    public float armor;

    public bool hasWeapon;

    public int deaths;
    public int kills;
    public int score;

    public WeaponTypes equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        armor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
