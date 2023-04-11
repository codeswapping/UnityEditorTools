using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HorizontalLineDemo))]
public class DemoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "allIndexes");

        var listProperty = serializedObject.FindProperty("allIndexes");
        EditorGUILayout.PropertyField(listProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}
