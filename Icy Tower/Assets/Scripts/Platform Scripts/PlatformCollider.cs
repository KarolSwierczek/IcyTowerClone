﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {

    private BoxCollider platform;

    void Start () {
        platform = transform.parent.gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.tag == "Player")
            {
                Physics.IgnoreCollision(other, platform, true);
            }
        }
        catch { }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Physics.IgnoreCollision(other, platform, false);
        }
    }
}