#if UNITY_EDITOR
using UnityEngine;
namespace Ghoulish.FixedCameraSystem
{
    [ExecuteInEditMode]
    public class ZoneEditorGizmo_Child : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            transform.parent.SendMessage("OnDrawGizmosSelected");
        }
    }
}
#endif