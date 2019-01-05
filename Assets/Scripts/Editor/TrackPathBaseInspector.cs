using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackPathBase))]
public class TrackPathBaseInspector : Editor
{
    public Vector3 snap = Vector3.one * 0.5f;

    // SerializedProperty PointA;
    // SerializedProperty PointB;
    // SerializedProperty Handle;
    // SerializedProperty Length;

    // void OnEnable()
    // {
    //     // Setup the SerializedProperties.
    //     PointA = serializedObject.FindProperty("PointA");
    //     PointB = serializedObject.FindProperty("PointB");
    //     Handle = serializedObject.FindProperty("Handle");
    //     Length = serializedObject.FindProperty("Length");
    // }

    // public override void OnInspectorGUI()
    // {
    //     serializedObject.Update();
    //     serializedObject.ApplyModifiedProperties();
    // }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrackPathBase tp = (TrackPathBase)target;

        tp.PointA = EditorGUILayout.Vector2Field("PointA", tp.PointA);
        tp.PointB = EditorGUILayout.Vector2Field("PointB", tp.PointB);
        EditorGUILayout.LabelField("Length", tp.Length.ToString());

        if (GUILayout.Button("Compute Length"))
        {
            tp.ComputeLength();
        }
    }

    void OnSceneGUI()
    {
        TrackPathBase tp = target as TrackPathBase;
        Vector3 pos3 = tp.gameObject.transform.position;

        Vector3 snap = Vector3.one * 0.5f;

        // Point A
        EditorGUI.BeginChangeCheck();
        Vector2 NewPointA = Handles.FreeMoveHandle(tp.PointA, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed PointA");
            tp.PointA = NewPointA;
            tp.ComputeLength();
        }
        // Point B
        EditorGUI.BeginChangeCheck();
        Vector2 NewPointB = Handles.FreeMoveHandle(tp.PointB, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed PointB");
            tp.PointB = NewPointB;
            tp.ComputeLength();
        }

        Handles.DrawLine(tp.PointA, tp.PointB); // HandleUtility.GetHandleSize(pos3)
    }
}
