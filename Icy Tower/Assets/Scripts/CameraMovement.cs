using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform Player;
    public float RisingSpeed = 0.01f;

    private float altitudeDifference;
    private new Transform camera;
    private bool canRise = false;
    private bool isDead = false;

	void Start () {
        camera = GetComponent<Transform>();
	}

    void Update() {

        if (canRise && !isDead) { 
        camera.Translate(new Vector3(0, RisingSpeed * camera.position.y / 120, 0));
        }

        altitudeDifference = Player.position.y - camera.position.y - 5.2f;

        if (altitudeDifference > 0)
        {
            camera.Translate(new Vector3(0, altitudeDifference, 0));
            canRise = true; //replace with a trigger collider
        }
        else if(altitudeDifference < -13.8)
        {
            isDead = true;
            //notify stats etc
        }

	}
}
