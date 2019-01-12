using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathCurve : TrackPathBase
{
    public float side = 1f;
    public Transform handle;
    public float degrees = 0f;
    public float radius = 1f;
    public float decelerationMod = 2f;

    [Range(0,50)]
    public int segments = 50;

    public override void ComputeValues()
    {
        Vector2 dirA = PointA.position - handle.position;
        degrees = Atan2_360(dirA);

        float l1 = Vector2.Distance(PointA.position, handle.position);
        float l2 = Vector2.Distance(PointB.position, handle.position);

        radius = (l1 + l2) / 2;

        _length = (2f * Mathf.PI * radius) / 4;
    }

    public override void UpdateLineRenderer() {
        CreatePoints();
    }

    public override void MoveTransform(Transform t, float distance, ref MoveData moveData) {
        float moveDist = distance / _length;
        float total = moveData.lastMove + moveDist;
        if (total > 1f) {
            moveDist = 1f;
            moveData.extra = total - 1f;
            total = 1f;
        }
        if (total < 0f) {
            moveDist = 0f;
            moveData.extra = total;
            total = 0f;
        }

        float angle = (total * 90f * side) + degrees;

        float x = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
        float y = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;

        t.position = handle.position + new Vector3(x, y, t.position.z);
        t.rotation = Quaternion.Euler(0f, 0f, angle);

        moveData.lastMove = Mathf.Clamp(moveData.lastMove + moveDist, 0f, 1f);

        // reduce speed
        moveData.speed -= (decelerationMod * (distance / radius)) / radius;
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeValues();
        UpdateLineRenderer();
        CreatePoints();
    }

    void CreatePoints ()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;

        float x;
        float y;

        float angle = degrees;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0f) + handle.position);

            angle += (90f * side) / segments;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    float Atan2_360(Vector2 v) {
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        return angle > 0 ? angle : 180 + (180 + angle);
    }
}
