﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathBezier //: TrackPathBase
{
    // private Vector2 _handle;
    // public Vector3 Handle
    // {
    //     get { return transform.position + (Vector3)_handle; }
    //     set { _handle = (Vector2)value - (Vector2)transform.position; }
    // }

    // public override void ComputeLength()
    // {
    //     // This is actually kinda nuts to do.
    //     // See:
    //     // Antinous (https://math.stackexchange.com/users/23431/antinous),
    //     // Arc Length of Bézier Curves, URL (version: 2018-10-19):
    //     // https://math.stackexchange.com/q/2339485
    //     //
    //     // I don't claim to understand this in the slightest.
    //     Vector2 v;
    //     Vector2 w;
    //     v.x = 2 * (PointB.position.x - PointA.position.x);
    //     v.y = 2 * (PointB.position.y - PointA.position.y);
    //     w.x = _handle.x - 2 * PointB.position.x + PointA.position.x;
    //     w.y = _handle.y - 2 * PointB.position.y + PointA.position.y;

    //     // What?
    //     float uu = 4 * (w.x*w.x + w.y*w.y);

    //     if  (uu < 0.00001)
    //     {
    //         _length = Mathf.Sqrt((_handle.x - PointA.position.x) * (_handle.x - PointA.position.x) + (_handle.y - PointA.position.y) * (_handle.y - PointA.position.y));
    //         return;
    //     }

    //     float vv = 4 * (v.x*w.x + v.y*w.y);
    //     float ww = v.x*v.x + v.y*v.y;

    //     float t1 = 2 * Mathf.Sqrt(uu * (uu + vv + ww));
    //     float t2 = 2 * uu + vv;
    //     float t3 = vv*vv - 4 * uu * ww;
    //     float t4 = (float) (2*Mathf.Sqrt(uu*ww));

    //     // ??????magic??
    //     _length = ((t1*t2 - t3*Mathf.Log(t2+t1) - (vv*t4 - t3*Mathf.Log(vv+t4))) / (8*Mathf.Pow(uu, 1.5f)));
    // }

    // public override void UpdateLineRenderer() {
    //     /// eh whatever
    // }

    // public override float MoveTransform(Transform t, float speed, float deltaTime, float lastMove) {
    //     return lastMove;
    // }

    // // Start is called before the first frame update
    // void Start()
    // {
    //     ComputeLength();
    //     UpdateLineRenderer();
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}

