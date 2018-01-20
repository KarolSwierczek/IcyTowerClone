using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInitialiser : MonoBehaviour {

    private new Transform transform;
    private static float stageWidth = 31.0f;
    private float[] widthRange;

    void Start()
    {
        transform = GetComponent<Transform>();
        widthRange = WidthRange(transform.position.y);
        transform.localScale = new Vector3(Random.Range(widthRange[0], widthRange[1]), 1.0f, 1.0f);
        float range = 0.95f * (stageWidth - transform.localScale.x) / 2;
        transform.Translate(new Vector3(Random.Range(-range, range), 0f, 0f));
    }


    private float[] WidthRange(float altitude)
    {
        float arctg = Mathf.Atan(-0.0022f * altitude);
        float min = 6 + 2.9f * arctg;
        float max = 18 + 9.6f * arctg;
        float[] range = new float[2] { min, max };
        return range;
    }
}
