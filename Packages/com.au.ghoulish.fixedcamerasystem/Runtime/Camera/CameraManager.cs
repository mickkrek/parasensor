using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Ghoulish.PlayerControls;
using Unity.Cinemachine;
using System;

namespace Ghoulish.FixedCameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        #region Singleton
        private static CameraManager _instance;
        private void Awake()
        {
            if (_instance == null || _instance == this)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public static CameraManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("CameraManager is NULL");
                return _instance;
            }
        }
        #endregion
        
        private CinemachineVirtualCameraBase _startCamera = null;
        private CinemachineVirtualCameraBase _storedVirtualCam;

        public IPlayerControls PlayerInput;
        [HideInInspector] public CinemachineVirtualCameraBase ActiveVirtualCam { get; set; }
        [HideInInspector] public CinemachineVirtualCameraBase ConversationOverrideCam { get; set; }
        [HideInInspector] public List<string> VisitedScenes;
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene current, LoadSceneMode mode)
        {
            _startCamera = FindFirstObjectByType<StartCamera>().GetComponentInChildren<CinemachineVirtualCameraBase>();
            if (_startCamera == null)
            {
                Debug.LogError("StartCamera not found in scene. You must apply a StartCamera component to one zone in scene.");
            }
            _startCamera.Priority = 1;
            _storedVirtualCam = _startCamera;
            ActiveVirtualCam = _startCamera;
        }
        private void OnSceneUnloaded(Scene current)
        {
            if (!VisitedScenes.Contains(current.name))
            {
                VisitedScenes.Add(current.name);
            }
        }
        public void UpdateActiveCamera()
        {
            //When a new camera is activated, deprioritise the old one and prioritise the new one
            if (_storedVirtualCam != ActiveVirtualCam)
            {
                if (_storedVirtualCam != null)
                {
                    _storedVirtualCam.Priority = 0;
                }
                ActiveVirtualCam.Priority = 1;
                _storedVirtualCam = ActiveVirtualCam;
            }
        }
    }
}