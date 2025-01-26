using UnityEngine;

[ExecuteInEditMode]
public class NPCEditorGizmo : MonoBehaviour
{
    #if UNITY_EDITOR
    private Collider _col;
    public Color GizmoColor = new Color(1f, 1f, 1f);

    void OnDrawGizmosSelected()
    {
        _col = GetComponentInChildren<Collider>();
        if (_col != null)
        {
            Gizmos.DrawIcon(_col.bounds.center, "Assets/Gizmos/NPCIcon.png", true, GizmoColor);
        }
    }
    #endif
}