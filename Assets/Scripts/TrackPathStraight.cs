using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathStraight : TrackPathBase
{
    private Vector2 handle;
    public Vector3 Handle
    {
        get { return transform.position + (Vector3)handle; }
        set { handle = (Vector2)value - (Vector2)transform.position; }
    }

    public override void ComputeLength()
    {
        _length = Vector2.Distance(_pointA, _pointB);
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeLength();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

