#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D.PSD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ghoulish.FixedCameraSystem
{
    public class ImportBackgrounds : EditorWindow
    {
        private string newAssetPath, sceneName;
        private bool zonesGathered = false;

        [MenuItem ("Ghoulish/Import Backgrounds")]
        public static void  ShowWindow () 
        {
            EditorWindow.GetWindow(typeof(ImportBackgrounds));
        }
        
        void OnGUI ()
        {
            if (SceneManager.GetSceneAt(1).name != "BaseScene")
            {
                sceneName = SceneManager.GetSceneAt(1).name;
            }
            GUIStyle myCustomStyle = new GUIStyle(GUI.skin.GetStyle("label"))
            {
                wordWrap = true
            };
            GUILayout.Label ("Import PSB Backgrounds", EditorStyles.largeLabel);
            GUILayout.Label ("This tool will import a PSB file into the active scene as a GameObject, then sorts the PSB layers correctly for rendering.", myCustomStyle);
            GUILayout.Label ("NOTE:", myCustomStyle);
            GUILayout.Label ("BaseScene must be the FIRST scene in the heirarchy,", myCustomStyle);
            GUILayout.Label ("your environment scene must be the SECOND scene in the heirarchy!", myCustomStyle);
            if (GUILayout.Button("Gather Zones from active scene"))
            {
                zonesGathered = true;
            }
            if (zonesGathered)
            {
                GUILayout.Label ("Choose a zone to import background for:", myCustomStyle);
                ZoneEditorGizmo[] ZoneGizmos = FindObjectsByType<ZoneEditorGizmo>(FindObjectsSortMode.None);
                for(int i = 0; i < ZoneGizmos.Length; i++)
                {
                    if (GUILayout.Button(ZoneGizmos[i].name))
                    {
                        ImportPSB(ZoneGizmos[i].name);
                        CreateBackgroundObjects(ZoneGizmos[i].name);
                    }
                }
            }
        }
        

        void ImportPSB(string ZoneName)
        {
            string sourcePath = EditorUtility.OpenFilePanel("Import PSB Background", "", "PSB");
            if (sourcePath.Length != 0)
            {
                string newFolderPath = "Assets/Scenes/"+sceneName+"/Backgrounds";
                newAssetPath = newFolderPath+"/"+ZoneName+".PSB";
                if(!Directory.Exists(newFolderPath)) 
                {
                    Directory.CreateDirectory(newFolderPath);
                }
                FileUtil.ReplaceFile(sourcePath, newAssetPath);
                AssetDatabase.Refresh();
            }
        }
        void CreateBackgroundObjects(string ZoneName)
        {
            GameObject sourcePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newAssetPath, typeof(GameObject));
            GameObject createdGO = PrefabUtility.InstantiatePrefab(sourcePrefab) as GameObject;
            createdGO.transform.SetParent(GameObject.Find(ZoneName).transform);
            AssignLayersToChildren(createdGO.transform.Find("Background"), LayerMask.NameToLayer("Background_2D"));
            AssignLayersToChildren(createdGO.transform.Find("Midground"), LayerMask.NameToLayer("Midground_2D"));
            AssignLayersToChildren(createdGO.transform.Find("Foreground"), LayerMask.NameToLayer("Foreground_2D"));
        }
        void AssignLayersToChildren(Transform prefabParent, int layer)
        {  
            if (prefabParent != null)
            {
                prefabParent.gameObject.layer = layer;
                foreach (Transform child in prefabParent.transform) 
                {
                    child.gameObject.layer = layer;
                }
            }
        }
    }
}
#endif