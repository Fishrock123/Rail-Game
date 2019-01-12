using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackPathBase : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float _length;

    public RailSystem rails;

    [HideInInspector]
    public float Length {
        get { return _length; }
    }

    public abstract void ComputeValues();
    public abstract void UpdateLineRenderer();
    public abstract void MoveTransform(Transform t, float distance, ref MoveData moveData);
}
