using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform Player;

    private float risingSpeed = 0.0f;
    private float altitudeDifference;
    private new Transform camera;

	void Start () {
        camera = GetComponent<Transform>();
	}
	
	void Update () {
        altitudeDifference = Player.position.y - camera.position.y - 5.2f;

        if (altitudeDifference > 0)
        {
            camera.Translate(new Vector3(0, altitudeDifference, 0));
        }

	}
}
