using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveData {
    public float lastMove;
    public float extra;
    public TrackPathBase currentTrack;
}

public abstract class TrackPathBase : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float _length;

    [HideInInspector]
    public float Length {
        get { return _length; }
    }

    public abstract void ComputeValues();
    public abstract void UpdateLineRenderer();
    public abstract void MoveTransform(Transform t, float distance, ref MoveData moveData);
}
