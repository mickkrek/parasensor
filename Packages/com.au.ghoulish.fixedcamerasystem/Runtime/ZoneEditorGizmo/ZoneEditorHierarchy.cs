#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace Ghoulish.FixedCameraSystem
{
    namespace HierarchyExtensions
    {
        [InitializeOnLoad]
        public class ZoneEditorHeirarchy
        {
            private static Color _iconColor = new Color(.2f, 1f, .2f);
            private static GUIStyle _labelStyle;
            private static int[] _visibleObjs;

            [InitializeOnLoadMethod]
            public static void Init()
            {
                EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItem_CB;
            }

            private static void HierarchyWindowItem_CB(int id, Rect rect)
            {
                if(_labelStyle == null)
                    _labelStyle = new GUIStyle(EditorStyles.label);

                var obj = EditorUtility.InstanceIDToObject(id);
                var go = obj as GameObject;

                if(go == null)
                    return;

                var zoneEditorGizmo = go.GetComponent<ZoneEditorGizmo>();

                if (zoneEditorGizmo == null)
                    return;

                if (Event.current.type != EventType.Repaint)
                    return;


                //Icon
                var iconRect = new Rect(rect);
                iconRect.width = 23;
                iconRect.height = 23;
                iconRect.x += -32f;
                iconRect.y += -3f;
                GUI.color = Color.white;
                if (go.GetComponent<StartCamera>())
                {
                    GUI.color = Color.green;
                }
                GUI.Label(iconRect, EditorGUIUtility.IconContent("Camera Icon"));
                GUI.color = Color.white;
                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
}
#endif