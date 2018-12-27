using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathDefinition : MonoBehaviour
{

    private Vector2 pointA;
    public Vector3 PointA
    {
        get { return transform.position + (Vector3)pointA; }
        set { pointA = (Vector2)value - (Vector2)transform.position; }
    }
    private Vector2 pointB;
    public Vector3 PointB
    {
        get { return transform.position + (Vector3)pointB; }
        set { pointB = (Vector2)value - (Vector2)transform.position; }
    }
    private Vector2 handle;
    public Vector3 Handle
    {
        get { return transform.position + (Vector3)handle; }
        set { handle = (Vector2)value - (Vector2)transform.position; }
    }

    [HideInInspector]
    public float Length;

    public void ComputeCurveLength()
    {
        // This is actually kinda nuts to do.
        // See:
        // Antinous (https://math.stackexchange.com/users/23431/antinous),
        // Arc Length of Bézier Curves, URL (version: 2018-10-19):
        // https://math.stackexchange.com/q/2339485
        //
        // I don't claim to understand this in the slightest.
        Vector2 v;
        Vector2 w;
        v.x = 2 * (PointB.x - PointA.x);
        v.y = 2 * (PointB.y - PointA.y);
        w.x = Handle.x - 2 * PointB.x + PointA.x;
        w.y = Handle.y - 2 * PointB.y + PointA.y;

        // What?
        float uu = 4 * (w.x*w.x + w.y*w.y);

        if  (uu < 0.00001)
        {
            Length = Mathf.Sqrt((Handle.x - PointA.x) * (Handle.x - PointA.x) + (Handle.y - PointA.y) * (Handle.y - PointA.y));
            return;
        }

        float vv = 4 * (v.x*w.x + v.y*w.y);
        float ww = v.x*v.x + v.y*v.y;

        float t1 = 2 * Mathf.Sqrt(uu * (uu + vv + ww));
        float t2 = 2 * uu + vv;
        float t3 = vv*vv - 4 * uu * ww;
        float t4 = (float) (2*Mathf.Sqrt(uu*ww));

        // ??????magic??
        Length = ((t1*t2 - t3*Mathf.Log(t2+t1) - (vv*t4 - t3*Mathf.Log(vv+t4))) / (8*Mathf.Pow(uu, 1.5f)));
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeCurveLength();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
