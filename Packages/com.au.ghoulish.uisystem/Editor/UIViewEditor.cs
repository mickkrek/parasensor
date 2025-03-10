#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Ghoulish.UISystem;

[CustomEditor (typeof (UIView), true)]
public class UIViewEditor: Editor
{
    SerializedProperty normal, hover, selected, submit, disabled;

    private string[] tabs = {"Normal", "Hover", "Selected", "Submit", "Disabled"};
    private int tabIndex = 0;
    private GUIStyle headerStyle;
    
    void OnEnable()
    {
        normal = serializedObject.FindProperty("normal");
        hover = serializedObject.FindProperty("hover");
        selected = serializedObject.FindProperty("selected");
        submit = serializedObject.FindProperty("submit");
        disabled = serializedObject.FindProperty("disabled");

        headerStyle = new GUIStyle();
        headerStyle.fontSize = 20;
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.alignment = TextAnchor.MiddleCenter;
        headerStyle.normal.textColor = Color.yellow;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField(serializedObject.FindProperty("ComponentName").stringValue, headerStyle);
        EditorGUILayout.BeginVertical();
        tabIndex = GUILayout.Toolbar(tabIndex, tabs);
        EditorGUILayout.EndVertical();
        switch (tabIndex)
        {
            case 0:
                if (normal != null) 
                {
                    EditorGUILayout.PropertyField(normal, new GUIContent("Settings:")); //TODO: Find a way to display only the properties, not the foldout collapsible
                }
                else
                {
                    EditorGUILayout.LabelField("This component has no 'Normal' setting.", EditorStyles.boldLabel);
                }
                break;
            case 1:
                if (hover != null) 
                {
                    EditorGUILayout.PropertyField(hover, new GUIContent("Settings:"));
                }
                else
                {
                    EditorGUILayout.LabelField("This component has no 'Hover' setting.", EditorStyles.boldLabel);
                }
                break;
            case 2:
                if (selected != null) 
                {
                    EditorGUILayout.PropertyField(selected, new GUIContent("Settings:"));
                }
                else
                {
                    EditorGUILayout.LabelField("This component has no 'Selected' setting.", EditorStyles.boldLabel);
                }
                break;
            case 3:
                if (submit != null)
                {
                    EditorGUILayout.PropertyField(submit, new GUIContent("Settings:"));
                }
                else
                {
                    EditorGUILayout.LabelField("This component has no 'Submit' setting.", EditorStyles.boldLabel);
                }
                break;
            case 4:
                if (disabled != null)
                {
                    EditorGUILayout.PropertyField(disabled, new GUIContent("Settings:"));
                }
                else
                {
                    EditorGUILayout.LabelField("This component has no 'Disabled' setting.", EditorStyles.boldLabel);
                }
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif