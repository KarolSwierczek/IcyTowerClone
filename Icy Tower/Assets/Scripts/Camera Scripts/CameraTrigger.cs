using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    private CameraMovement cameraMovement;

    void Start()
    {
        cameraMovement = GameObject.Find("Cameras").GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.tag == "Player")
            {
                cameraMovement.SetCanRise(true);
            }
        }
        catch { }
    }
}
