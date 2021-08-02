using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineControlRemote : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(transform.root.GetComponent<PlayerManager>().spineAngle, 0f, 0f);
    }
}

