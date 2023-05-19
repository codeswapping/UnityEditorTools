using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SerializableMonoBehaviour), true)]
public class SerializableDictionaryEditor : Editor
{
	bool isShowing = true;
	public override void OnInspectorGUI()
	{
		Debug.Log("Checking editor");
		serializedObject.Update();
		EditorGUI.BeginChangeCheck();
		var serialized = serializedObject.GetIterator();
		if (serialized.NextVisible(true))
		{
			do
			{
				if (serialized.propertyType == SerializedPropertyType.Generic)
				{
					if (serialized.type.Contains("SerializableDictionary"))
					{
						EditorGUILayout.BeginHorizontal();

						isShowing = EditorGUILayout.Foldout(isShowing, serialized.name, true);
						GUILayout.Space(5);
						var style = new GUIContent("+", "Add new item in the dictionary");
						var keys = serialized.FindPropertyRelative("keys");
						var values = serialized.FindPropertyRelative("values");
						var keyElementType = keys.arrayElementType;
						var valueElementType = values.arrayElementType;

						EditorGUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();

						EditorGUILayout.LabelField("Size", GUILayout.Height(20), GUILayout.Width(40), GUILayout.MaxWidth(40));
						var size = EditorGUILayout.IntField(keys.arraySize, GUILayout.Width(40), GUILayout.MaxWidth(40));
						if (GUILayout.Button(style, GUILayout.Width(20)))
						{
							if (keys.arraySize == 0)
							{
								keys.arraySize = 1;
							}
							else
							{
								keys.InsertArrayElementAtIndex(keys.arraySize - 1);
								var key = keys.GetArrayElementAtIndex(keys.arraySize - 1);
								for (int i = 0; i < keys.arraySize - 1; i++)
								{
									var k = keys.GetArrayElementAtIndex(i);
									switch (key.propertyType)
									{
										case SerializedPropertyType.Integer:
											if (k.intValue == key.intValue)
											{
												key.intValue = key.intValue + Random.Range(1, 5000);
											}
											break;
										case SerializedPropertyType.Float:
											if (k.floatValue == key.floatValue)
											{
												key.floatValue = key.floatValue + Random.Range(1, 5000);
											}
											break;
										case SerializedPropertyType.String:
											if (k.stringValue.Equals(key.stringValue))
											{
												key.stringValue = key.stringValue + Random.Range(1, 5000);
											}
											break;
										default:
											break;
									}
								}
							}
							values.arraySize = keys.arraySize;
							size = keys.arraySize;
						}
						EditorGUILayout.EndHorizontal();
						keys.arraySize = size;
						values.arraySize = size;
						for (int i = 0; i < keys.arraySize - 1; i++)
						{
							var key = keys.GetArrayElementAtIndex(i);
							for (int j = 0; j < keys.arraySize; j++)
							{
								if (j == i)
									continue;
								var k = keys.GetArrayElementAtIndex(j);
								switch (key.propertyType)
								{
									case SerializedPropertyType.Integer:
										if (k.intValue == key.intValue)
										{
											key.intValue = key.intValue + Random.Range(1, 5000);
										}
										break;
									case SerializedPropertyType.Float:
										if (k.floatValue == key.floatValue)
										{
											key.floatValue = key.floatValue + Random.Range(1, 5000);
										}
										break;
									case SerializedPropertyType.String:
										if (k.stringValue.Equals(key.stringValue))
										{
											key.stringValue = key.stringValue + Random.Range(1, 5000);
										}
										break;
									default:
										break;
								}
							}
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space(5);
						if (isShowing)
						{
							//EditorGUI.indentLevel++;
							values.arraySize = keys.arraySize;
							if (keys.arraySize > 0)
							{
								for (int i = 0; i < keys.arraySize; i++)
								{
									var rect = GUILayoutUtility.GetLastRect();
									rect.y += 5;
									rect.x -= 2;
									rect.width = EditorGUIUtility.currentViewWidth - 30;
									rect.height = 1;
									EditorGUI.DrawRect(rect, Color.green);
									GUILayout.Space(5);
									//var rect = new Rect(0, GUILayoutUtility.GetLastRect().y,0, 20);
									EditorGUILayout.BeginHorizontal(GUILayout.Width(EditorGUIUtility.currentViewWidth - 50));
									var key = keys.GetArrayElementAtIndex(i);
									var value = values.GetArrayElementAtIndex(i);
									var width = ((EditorGUIUtility.currentViewWidth - 40) / 2f);
									//rect.width = width * 2f;
									//EditorGUI.DrawRect(rect,Color.cyan);
									switch (key.propertyType)
									{
										case SerializedPropertyType.Integer:
											EditorGUILayout.LabelField("Key", GUILayout.Height(20), GUILayout.Width(50));
											key.intValue = EditorGUILayout.IntField(key.intValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										case SerializedPropertyType.Float:
											EditorGUILayout.LabelField("Key", GUILayout.Height(20), GUILayout.Width(50));
											key.floatValue = EditorGUILayout.FloatField(key.floatValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										case SerializedPropertyType.String:
											EditorGUILayout.LabelField("Key", GUILayout.Height(20), GUILayout.Width(50));
											key.stringValue = EditorGUILayout.TextField(key.stringValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										default:
											EditorGUILayout.LabelField("Key", GUILayout.Height(20), GUILayout.Width(50));
											EditorGUILayout.PropertyField(key, GUILayout.MaxWidth(width * 2 - 50));
											break;
									}
									if (GUILayout.Button("X", GUILayout.Width(30), GUILayout.MaxWidth(20), GUILayout.Height(20)))
									{
										keys.DeleteArrayElementAtIndex(i);
										values.DeleteArrayElementAtIndex(i);
									}
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal(GUILayout.Width(EditorGUIUtility.currentViewWidth - 20));
									switch (value.propertyType)
									{
										case SerializedPropertyType.Integer:
											EditorGUILayout.LabelField("Value", GUILayout.Height(20), GUILayout.Width(50));
											value.intValue = EditorGUILayout.IntField(value.intValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										case SerializedPropertyType.Float:
											EditorGUILayout.LabelField("Value", GUILayout.Height(20), GUILayout.Width(50));
											value.floatValue = EditorGUILayout.FloatField(value.floatValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										case SerializedPropertyType.String:
											EditorGUILayout.LabelField("Value", GUILayout.Height(20), GUILayout.Width(50));
											value.stringValue = EditorGUILayout.TextField(value.stringValue, GUILayout.Height(20), GUILayout.MaxWidth(width - 50));
											break;
										default:
											EditorGUILayout.LabelField("Value", GUILayout.Height(20), GUILayout.Width(50));
											EditorGUILayout.PropertyField(value, GUILayout.MaxWidth(width * 2 - 50));
											break;
									}
									EditorGUILayout.EndHorizontal();
									var rect1 = GUILayoutUtility.GetLastRect();
									rect1.x -= 2;
									rect1.width = 1;
									rect1.height += rect1.y - rect.y + 6;
									rect1.y = rect.y;
									EditorGUI.DrawRect(rect1, Color.green);
									rect1.x = EditorGUIUtility.currentViewWidth - 15;
									EditorGUI.DrawRect(rect1, Color.green);
									GUILayout.Space(10);
									rect = GUILayoutUtility.GetLastRect();
									rect.x -= 2;
									rect.y += 5;
									rect.width = EditorGUIUtility.currentViewWidth - 30;
									rect.height = 1;
									EditorGUI.DrawRect(rect, Color.green);
								}
							}
							//EditorGUI.indentLevel--;
						}
					}
				}
				else
				{
					EditorGUILayout.PropertyField(serialized, true);
				}
			}
			while (serialized.NextVisible(false));
		}
		serializedObject.ApplyModifiedProperties();
		if (EditorGUI.EndChangeCheck())
		{
			EditorUtility.SetDirty(target);
		}
	}
}
