using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int platformNumber = 0;

    private void OnCollisionEnter(Collision collision)
    {
        platformNumber = (int)collision.transform.position.y / 4;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 120, 20), "Platform number: " + platformNumber.ToString());
    }
}
