﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathCurve : TrackPathBase
{
    // false = left, true = right
    public bool side = true;
    public Transform handle;
    public Transform slerpPointB;

    [Range(0,50)]
    public int segments = 50;
    [Range(0,5)]
    public float xradius = 1;
    [Range(0,5)]
    public float yradius = 1;

    public override void ComputeLength()
    {
        // float cLength = Vector2.Distance(PointA.position, PointB.position);
        // float abLength = Mathf.Sqrt(Mathf.Pow(cLength, 2) / 2);

        // Quaternion.AngleAxis(side ? 45f : -45f, Vector3.forward);

        float l1 = Vector2.Distance(PointA.position, handle.position);
        float l2 = Vector2.Distance(PointB.position, handle.position);

        _length = (2f * Mathf.PI * ((l1 + l2) / 2)) / 4;
        Debug.Log(_length);
    }

    public override void UpdateLineRenderer() {
        LineRenderer line = GetComponent<LineRenderer>();

        line.SetPosition(0, PointA.position);
        line.SetPosition(1, PointB.position);
    }

    public override void MoveTransform(Transform t, float distance, ref MoveData moveData) {
        float moveDist = distance / _length;
        // Debug.Log(moveDist);
        float total = moveData.lastMove + moveDist;
        if (total > 1f) {
            moveDist = 1f;
            moveData.extra = total - 1f;
            total = 1f;
        }

        float radius = Vector2.Distance(PointA.position, handle.position);

        float angle = Vector2.Angle(handle.localPosition, PointA.localPosition) + (total * 90f) - 90f;

        float x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
        float y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

        t.position = handle.position + new Vector3(x, y, t.position.z);
        t.rotation = Quaternion.Euler(0f, 0f, -angle);

        moveData.lastMove += moveDist;
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeLength();
        UpdateLineRenderer();
        CreatePoints();
    }

    void CreatePoints ()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        float x;
        float y;

        float angle = Vector2.Angle(handle.localPosition, PointA.localPosition) - 180f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i, new Vector3(x, y, 0f) + handle.localPosition);

            angle += (90f / segments);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
