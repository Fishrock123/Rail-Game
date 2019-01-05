using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackPathBase : MonoBehaviour
{
    public Vector2 _pointA;
    public Vector2 _pointB;
    public float _length;

    public Vector3 PointA
    {
        get { return transform.position + (Vector3)_pointA; }
        set { _pointA = (Vector2)value - (Vector2)transform.position; }
    }
    public Vector3 PointB
    {
        get { return transform.position + (Vector3)_pointB; }
        set { _pointB = (Vector2)value - (Vector2)transform.position; }
    }

    [HideInInspector]
    public float Length {
        get { return _length; }
    }

    public abstract void ComputeLength();
}
