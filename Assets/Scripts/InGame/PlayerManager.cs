using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    public PlayerStats stats;
    public MeshRenderer model;

    public float spineAngle;

    public bool[] keysPressed;
    public bool isGrounded;

    public GameObject itemPrefab;

    public void Initialize(int id, string username, int deaths, int kills, int score)
    {
        this.id = id;
        this.username = username;
        stats.deaths = deaths;
        stats.kills = kills;
        stats.score = score;
        keysPressed = new bool[7];
    }

    public void SetKeysPressed(bool[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keysPressed[i] = keys[i];
        }
    }

    public void SetHealth(float health)
    {
        stats.health = health;
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Shoot()
    {
        // if (GameManager.instance.matchStage == MatchStage.match)
            //remainingAmmo = remainingAmmo < 1 ? maxAmmo : remainingAmmo - 1;
    }

    public void ItemPickedUp()
    {
        if (id == Client.instance.gameId)
        {
            Transform camera = RecursiveFindChild(transform.root, "MainCamera");

            GameObject item = Instantiate(itemPrefab, new Vector3(0.156f, 0.34f, 0.036f), Quaternion.Euler(0, 180, -90));

            item.transform.parent = camera;
            item.transform.localPosition = new Vector3(0.32f, -0.48f, 0.67f);
            item.transform.localRotation = Quaternion.Euler(-0.788f, 85.889f, -3.845f);
            item.transform.GetComponent<MeshCollider>().enabled = false;
            stats.hasWeapon = true;

            Debug.Log("Player has collected a weapon!");
        } 
        else
        {
            Transform righthand = RecursiveFindChild(this.transform.root, "RightHand");

            GameObject item = Instantiate(itemPrefab, new Vector3(0.156f, 0.34f, 0.036f), Quaternion.Euler(0, 180, -90));

            item.transform.parent = righthand;
            item.transform.localPosition = new Vector3(0.156f, 0.34f, 0.036f);
            item.transform.localRotation = Quaternion.Euler(0, 180, -90);
            item.transform.GetComponent<MeshCollider>().enabled = false;

            stats.hasWeapon = true;

            Debug.Log("Player has collected a weapon!");
        }
    }



    Transform RecursiveFindChild(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.tag == tag)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, tag);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}
