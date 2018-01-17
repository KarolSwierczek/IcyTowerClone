using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour {

    private Transform player;
    private int bottomLevel = 0;
    private Dictionary<string, Transform> transforms;

    private Object bottomSegment = null;
    private Object topSegment = null;

    public string[] SegmentOrder =
        {"Bottom", "Normal 1", "Normal 1", "Normal 1", "Normal 1", "Normal 0", "Normal 1", "Normal 1", "Normal 1", "Normal 1",
        "Ice 0", "Ice 1", "Ice 1", "Ice 1", "Ice 1", "Ice 0", "Ice 1" , "Ice 1", "Ice 1", "Ice 1",
        "Sand 0", "Sand 1", "Sand 1", "Sand 1", "Sand 1", "Sand 0", "Sand 1", "Sand 1", "Sand 1", "Sand 1", "Top"};

    //public Transform Bottom;
    //public Transform Top;
    //public Transform Normal0;
    //public Transform Normal1;
    //public Transform Ice0;
    //public Transform Ice1;
    //public Transform Sand0;
    //public Transform Sand1;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

        //transforms = new Dictionary<string, Transform>();
        //transforms.Add("Bottom", Bottom);
        //transforms.Add("Top", Top);
        //transforms.Add("Normal 0", Normal0);
        //transforms.Add("Normal 1", Normal1);
        //transforms.Add("Ice 0", Ice0);
        //transforms.Add("Ice 1", Ice1);
        //transforms.Add("Sand 0", Sand0);
        //transforms.Add("Sand 1", Sand1);

        bottomSegment = Instantiate(Resources.Load<GameObject>("Prefabs/Bottom") as GameObject, new Vector3(0,0,0), Quaternion.identity);
        topSegment = Instantiate(Resources.Load<GameObject>("Prefabs/Normal 1") as GameObject, new Vector3(0, 40, 0), Quaternion.identity);
        bottomLevel = 40;
    }

    private void Update()
    {
        if (player.position.y >= bottomLevel+20 && player.position.y < 29*40)
        {
            bottomLevel += 40;
            Destroy(bottomSegment);
            bottomSegment = topSegment;
            topSegment = Instantiate(Resources.Load<GameObject>("Prefabs/" + SegmentOrder[bottomLevel/40]) as GameObject, new Vector3(0, bottomLevel, 0), Quaternion.identity);
        }
    }


}
