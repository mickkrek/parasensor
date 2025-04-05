using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;

public class StripCollidersFromObjects : EditorWindow
{
    [MenuItem("Ghoulish/Strip Colliders From Selected Objects")]
    private static void StripColliders()
    {
        foreach(Transform selected in Selection.transforms)
        {
            Collider col = selected.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
            }
        }
    }
}