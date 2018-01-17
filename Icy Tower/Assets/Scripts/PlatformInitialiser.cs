using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInitialiser : MonoBehaviour {

    private new Transform transform;
    private static float stageWidth = 31.0f;
    private float minPlatformWidth = 6.0f;
    private float maxPlatformWidth = 15.0f;

    void Start()
    {
        transform = GetComponent<Transform>();
        transform.localScale = new Vector3(Random.Range(minPlatformWidth, maxPlatformWidth), 1.0f, 1.0f);
        float range = 0.95f * (stageWidth - transform.localScale.x) / 2;
        transform.Translate(new Vector3(Random.Range(-range, range), 0f, 0f));
    }

    //Returns a random float from <a, b>. Numbers follow normal distribution. 
    private float NormalDistribution(float a, float b)
    {
        return Random.Range(a, b);
    }
}
