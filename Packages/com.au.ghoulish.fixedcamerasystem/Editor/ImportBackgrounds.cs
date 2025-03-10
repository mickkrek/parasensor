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
        
        void OnGUI () {
            if (SceneManager.GetActiveScene().name != sceneName)
            {
                sceneName = SceneManager.GetActiveScene().name;
            }
            GUILayout.Label ("Import PSB Backgrounds", EditorStyles.boldLabel);
            GUILayout.Label ("This tool will import a PSB file into the active scene as a GameObject, then sorts the PSB layers correctly for rendering.", EditorStyles.label);
            if (GUILayout.Button("Gather Zones from active scene"))
            {
                zonesGathered = true;
            }
            if (zonesGathered)
            {
                GUILayout.Label ("Choose a zone to import background for:", EditorStyles.boldLabel);
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
                newAssetPath = "Assets/Scenes/"+sceneName+"/"+ZoneName+"/BG_"+ZoneName+".PSB";
                FileUtil.ReplaceFile(sourcePath, newAssetPath);
                AssetDatabase.Refresh();
            }
        }
        void CreateBackgroundObjects(string ZoneName)
        {
            GameObject sourcePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newAssetPath, typeof(GameObject));
            GameObject createdGO = PrefabUtility.InstantiatePrefab(sourcePrefab) as GameObject;
            createdGO.transform.SetParent(GameObject.Find("Backgrounds").transform);
            AssignLayersToChildren(createdGO.transform.Find("Background"), LayerMask.NameToLayer("Sprite Background"));
            AssignLayersToChildren(createdGO.transform.Find("Midground"), LayerMask.NameToLayer("Sprite Midground"));
            AssignLayersToChildren(createdGO.transform.Find("Foreground"), LayerMask.NameToLayer("Sprite Foreground"));
        }
        void AssignLayersToChildren(Transform prefabParent, int layer)
        {  
            prefabParent.gameObject.layer = layer;
            foreach (Transform child in prefabParent.transform) 
            {
                child.gameObject.layer = layer;
            }
        }
    }
}
#endif