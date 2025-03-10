#if UNITY_EDITOR
using UnityEngine;
namespace Ghoulish.FixedCameraSystem
{
    [ExecuteInEditMode]
    public class ZoneEditorGizmo : MonoBehaviour
    {
    
        private Collider[] _cols;
        void OnDrawGizmosSelected()
        {
            _cols = transform.GetComponentsInChildren<Collider>();
            foreach(Collider col in _cols) 
            {
                if (!col.GetComponent<ZoneEditorGizmo_Child>())
                {
                    col.gameObject.AddComponent<ZoneEditorGizmo_Child>();
                }

                Gizmos.color = new Color(.2f, 1f, .0f, .6f);
                Gizmos.DrawCube(col.bounds.center, col.bounds.size);
            }
        }
    }
}
#endif