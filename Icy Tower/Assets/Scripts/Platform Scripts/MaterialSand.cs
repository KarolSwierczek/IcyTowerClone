using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSand : MonoBehaviour {

    private Controller player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.Acceleration = 28.0f;
        player.Deceleration = 0.5f;
        player.ReverseSpeed = 0.8f;
    }
}
