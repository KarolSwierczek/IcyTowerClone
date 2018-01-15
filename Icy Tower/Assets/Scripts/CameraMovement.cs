using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform Player;

    private float _risingSpeed = 0.0f;
    private float _altitudeDifference;
    private Transform _camera;

	void Start () {
        _camera = GetComponent<Transform>();
	}
	
	void Update () {
        _altitudeDifference = Player.position.y - _camera.position.y - 5.2f;

        if (_altitudeDifference > 0)
        {
            _camera.Translate(new Vector3(0, _altitudeDifference, 0));
        }

	}
}
