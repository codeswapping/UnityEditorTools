using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace SAPUnityEditorTools.Editor
{
    [CustomPropertyDrawer(typeof(SAPUnityEditorTools.Tools.SAPList<>))]
    public class CustomListDrawer : PropertyDrawer
    {
        private const int buttonWidth = 18;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEngine.Debug.Log("In On GUI");
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.indentLevel++;
            int indent = EditorGUI.indentLevel * 15;

            Rect buttonPosition = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, EditorGUIUtility.singleLineHeight);

            position.width -= buttonWidth;

            EditorGUI.LabelField(position, label);

            SerializedProperty sizeProperty = property.FindPropertyRelative("Array.size");

            EditorGUI.PropertyField(position, sizeProperty, GUIContent.none);

            position.x += indent;
            position.width -= indent;

            EditorGUI.indentLevel--;

            for (int i = 0; i < sizeProperty.intValue; i++)
            {
                SerializedProperty elementProperty = property.GetArrayElementAtIndex(i);
                Rect elementPosition = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(elementPosition, elementProperty, GUIContent.none);
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            if (GUI.Button(buttonPosition, new GUIContent("A")))
            {
                property.InsertArrayElementAtIndex(property.arraySize);
            }

            if (GUI.Button(new Rect(buttonPosition.x, buttonPosition.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, buttonPosition.width, buttonPosition.height), new GUIContent("R")))
            {
                if (property.arraySize > 0)
                {
                    property.DeleteArrayElementAtIndex(property.arraySize - 1);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty sizeProperty = property.FindPropertyRelative("Array.size");
            return (sizeProperty.intValue * EditorGUIUtility.singleLineHeight) + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 3;
        }
    }
}