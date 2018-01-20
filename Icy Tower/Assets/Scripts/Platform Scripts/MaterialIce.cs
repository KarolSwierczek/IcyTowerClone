using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialIce : MonoBehaviour {

    private Controller player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.Acceleration = 42.0f;
        player.ReverseSpeed = 0.1f;
        player.Deceleration = 0.03f;
    }
}
