using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class OrthoCamAspectRatio : MonoBehaviour
{
    private readonly float horizontalResolution = 1920;
    private readonly float verticalResolution = 1080;

    public void UpdateOrthoSize()
    {
        float currentAspect = (float) Screen.width / (float) Screen.height;
        GetComponent<Camera>().orthographicSize = horizontalResolution / verticalResolution / currentAspect;
    }
    void Start()
    {
        UpdateOrthoSize();
    }
#if UNITY_EDITOR
    private Vector2 resolution;
    private void Awake()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }
    void OnGUI ()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            UpdateOrthoSize();
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }
#endif //UNITY_EDITOR
}

// Class only declared if we are in editor
#if UNITY_EDITOR
[CustomEditor(typeof(OrthoCamAspectRatio))]
public class OrthoCamAspectRatioEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This component resizes the orthographic camera to fit the current screen aspect ratio. It assumes your background sprites are 3840x2160px pixels! -Mickey", MessageType.Info, true);
    }
}
#endif