using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackPathBezier))]
public class TrackPathBezierInspector : TrackPathBaseInspector
{
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

        TrackPathBezier tp = (TrackPathBezier)target;

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
        TrackPathBezier tp = target as TrackPathBezier;
        Vector3 pos3 = tp.gameObject.transform.position;

        // Handle
        EditorGUI.BeginChangeCheck();
        Vector2 NewHandle = Handles.FreeMoveHandle(tp.Handle, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Handle");
            tp.Handle = NewHandle;
            tp.ComputeLength();
        }

        Handles.DrawBezier(tp.PointA, tp.PointB, tp.Handle, tp.Handle, Color.red, null, HandleUtility.GetHandleSize(pos3));
    }
}
