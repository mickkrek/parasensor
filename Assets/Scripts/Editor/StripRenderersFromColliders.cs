using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder;

public class StripRenderersFromColliders : EditorWindow
{
    [MenuItem("Ghoulish/Strip Renderers From Selected Colliders")]
    private static void StripRenderers()
    {
        foreach(Transform selected in Selection.transforms)
        {
            Collider col = selected.GetComponent<Collider>();
            if (col != null)
            {
                if (col.GetComponent<ProBuilderMesh>())
                {
                    //Probuilder mesh renderers cannot be destroyed
                    selected.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    DestroyImmediate(selected.GetComponent<MeshRenderer>());
                    if(col.GetType() != typeof(MeshCollider))
                    {
                        DestroyImmediate(selected.GetComponent<MeshFilter>());
                    }
                }
            }
        }
    }
}