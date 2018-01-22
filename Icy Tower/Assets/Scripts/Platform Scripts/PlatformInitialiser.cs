using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script randomizes the width and x transform of each platform.
public class PlatformInitialiser : MonoBehaviour {

    private new Transform transform;
    private static float stageWidth = 31.0f; //The distance between the walls.
    private float[] widthRange; //The range of platform widths.

    void Start()
    {
        transform = GetComponent<Transform>();
        widthRange = WidthRange(transform.position.y);
        transform.localScale = new Vector3(Random.Range(widthRange[0], widthRange[1]), 1.0f, 1.0f);
        float range = 0.95f * (stageWidth - transform.localScale.x) / 2;
        transform.Translate(new Vector3(Random.Range(-range, range), 0f, 0f));
    }

    //This function calculates the widthRange.
    //It's using arctan function to get a gradual decrease in min and max platform width.
    //Notice that since arctan is asympthotic, this function never returns a range smaller than roughlty <1.5, 3>,
    //even if there are infinity segments.
    //The max width decreases much faster than the min width increases.
    private float[] WidthRange(float altitude)
    {
        float arctg = Mathf.Atan(-0.0022f * altitude);
        float min = 6 + 2.9f * arctg;
        float max = 18 + 9.6f * arctg;
        float[] range = new float[2] { min, max };
        return range;
    }
}
