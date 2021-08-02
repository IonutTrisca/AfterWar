using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int spawnerId;
    public bool hasItem;
    public MeshRenderer itemModel;
    private Vector3 position;

    public float itemRotationSpeed = 50f;
    public float itemBobSpeed = 2f;


    private void Update()
    {
        if (hasItem)
        {
            transform.Rotate(Vector3.up, itemRotationSpeed * Time.deltaTime, Space.World);
            transform.position = position + new Vector3(0f, 0.25f * Mathf.Sin(Time.time * itemBobSpeed), 0);
        }
    }

    public void Intitialize(int spawnerId, bool hasItem, Vector3 position)
    {
        this.spawnerId = spawnerId;
        itemModel.enabled = hasItem;
        this.hasItem = hasItem;
        this.position = position;
    }

    public void ItemSpawned()
    {
        hasItem = true;
        itemModel.enabled = true;
    }

    public void ItemPickedUp()
    {
        hasItem = false;
        itemModel.enabled = false;
    }
}
