using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActionList))]
public class ActionListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty nodesList = serializedObject.FindProperty("Actions");
        EditorGUILayout.PropertyField(nodesList);

        //EditorGUILayout.BeginHorizontal();
        //using flexible space at the beginning and end of layout centers the buttons
        GUILayout.FlexibleSpace();

        TypeCache.TypeCollection actionTypes = TypeCache.GetTypesDerivedFrom(typeof(ActionNode));

        foreach (var actionType in actionTypes)
        {
            if (GUILayout.Button(text: "Add " + actionType.Name))
            {
                var index = nodesList.arraySize;
                nodesList.InsertArrayElementAtIndex(index);

                var property = nodesList.GetArrayElementAtIndex(index);
                property.managedReferenceValue = Activator.CreateInstance(actionType);

                serializedObject.ApplyModifiedProperties();
            }
        }

        GUILayout.FlexibleSpace();
        //EditorGUILayout.EndHorizontal();
    }
}
