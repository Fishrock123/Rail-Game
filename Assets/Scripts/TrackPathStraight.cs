using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathStraight : TrackPathBase
{
    public override void ComputeLength()
    {
        _length = Vector2.Distance(PointA.position, PointB.position);
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
        if (total < 0f) {
            moveDist = 0f;
            moveData.extra = total;
            total = 0f;
        }

        Vector2 lerpedPos = Vector2.Lerp(PointA.position, PointB.position, total);

        t.position = new Vector3(lerpedPos.x, lerpedPos.y, t.position.z);

        moveData.lastMove = Mathf.Clamp(moveData.lastMove + moveDist, 0f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeLength();
        UpdateLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

