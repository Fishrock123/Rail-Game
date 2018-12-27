using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrackPathDefinition))]
public class TrackPathInspector : Editor
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

        TrackPathDefinition tpd = (TrackPathDefinition)target;

        tpd.PointA = EditorGUILayout.Vector3Field("PointA", tpd.PointA);
        tpd.PointB = EditorGUILayout.Vector3Field("PointB", tpd.PointB);
        tpd.Handle = EditorGUILayout.Vector3Field("Handle", tpd.Handle);
        EditorGUILayout.LabelField("Length", tpd.Length.ToString());

        if (GUILayout.Button("Compute Curve Length"))
        {
            tpd.ComputeCurveLength();
        }
    }

    void OnSceneGUI()
    {
        TrackPathDefinition tpd = target as TrackPathDefinition;
        Vector3 pos3 = tpd.gameObject.transform.position;

        Vector3 snap = Vector3.one * 0.5f;

        // Point A
        EditorGUI.BeginChangeCheck();
        Vector2 NewPointA = Handles.FreeMoveHandle(tpd.PointA, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed PointA");
            tpd.PointA = NewPointA;
            tpd.ComputeCurveLength();
        }
        // Point B
        EditorGUI.BeginChangeCheck();
        Vector2 NewPointB = Handles.FreeMoveHandle(tpd.PointB, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed PointB");
            tpd.PointB = NewPointB;
            tpd.ComputeCurveLength();
        }
        // Handle
        EditorGUI.BeginChangeCheck();
        Vector2 NewHandle = Handles.FreeMoveHandle(tpd.Handle, Quaternion.identity, HandleUtility.GetHandleSize(pos3), snap, Handles.ArrowHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Handle");
            tpd.Handle = NewHandle;
            tpd.ComputeCurveLength();
        }

        Handles.DrawBezier(tpd.PointA, tpd.PointB, tpd.Handle, tpd.Handle, Color.red, null, HandleUtility.GetHandleSize(pos3));
    }
}
