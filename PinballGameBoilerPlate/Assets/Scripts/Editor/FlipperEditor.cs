using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Flipper))]
public class FlipperEditor : Editor
{
    private static float lineLength = 3.5f;
    private static float arcRadius = 3.0f;

    private static Color lineColor = new Color (1.0f, 1.0f, 0.0f, 1.0f);
    private static Color arcColor = new Color(0.0f, 1.0f, 0.0f, 0.2f);

    void OnSceneGUI() {
        Flipper flipper = (Flipper)target;
        
        Handles.color = lineColor;
        Handles.DrawLine(flipper.transform.position, (Vector2)flipper.transform.position + flipper.ClosedVector * lineLength);
        Handles.DrawLine(flipper.transform.position, (Vector2)flipper.transform.position + flipper.OpenVector * lineLength * Mathf.Sign(flipper.transform.localScale.y));

        Handles.color = arcColor;
        Handles.DrawSolidArc(flipper.transform.position, Vector3.forward, flipper.ClosedVector, flipper.range, arcRadius);
    }
}
