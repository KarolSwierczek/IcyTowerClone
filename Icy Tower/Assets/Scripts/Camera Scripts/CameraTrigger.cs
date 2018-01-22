using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script lets the CameraMovement script know when the cameras can begin rising.
//Theres a trigger collider on the bottom segment that triggers this event.
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
