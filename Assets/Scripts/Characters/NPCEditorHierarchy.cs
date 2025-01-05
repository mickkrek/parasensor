#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace HierarchyExtensions
{
    [InitializeOnLoad]
    public class NPCEditorHeirarchy
    {
        private static Color _bgColor = new Color(0f, 1f, 0f, .1f);
        private static Color _iconColor = new Color(.2f, 1f, .2f);
        private static GUIStyle _labelStyle;
        private static int[] _visibleObjs;
        private static GUIContent _icon;

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

            var NPCEditorGizmo = go.GetComponent<NPCEditorGizmo>();

            if (NPCEditorGizmo == null)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            bool selected = Selection.objects.Contains(obj);

            //Background
            var backgroundRect = new Rect(rect);
            backgroundRect.x = 32f;
            backgroundRect.width += 512f;
            backgroundRect.height -= 2;
            backgroundRect.y += 1f;
            GUI.backgroundColor = NPCEditorGizmo.GizmoColor * new Color(1f,1f,1f,0.1f);
            GUI.Box(backgroundRect, GUIContent.none, "OverrideMargin");

            //Icon
            GUI.color = NPCEditorGizmo.GizmoColor;
            var iconRect = new Rect(rect);
            iconRect.width = 20;
            iconRect.height = 20;
            iconRect.x += -32f;
            iconRect.y += -2f;
            GUI.Label(iconRect, EditorGUIUtility.IconContent("Assets/Gizmos/NPCIcon.png"));

            GUI.backgroundColor = Color.white;
            GUI.color = Color.white;
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}
#endif