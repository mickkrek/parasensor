using System;
using System.Collections.Generic;
using Pixelplacement;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    
    public AudioNode[] nodes;
    private Transform _listenerTransform;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AnimationCurve _falloffCurve;

    [Serializable]
    public struct AudioNode
    {
        public Vector3 position;
        public float maxRange;
        [Range(0,1)]public float volume;
    }
    void Start()
    {
        _listenerTransform = GameManager.Instance.CharacterController.transform;
    }

    void Update()
    {
        List<AudioNode> nodesWithinRange = new();
        AudioNode nearestNodeA = nodes[0];
        float nearestNodeADistance = 1000f;
        AudioNode nearestNodeB = nodes[1];
        float nearestNodeBDistance = 1001f;
        
        for (int i = 0; i < nodes.Length; i++)
        {
            float dist = Vector3.Distance(_listenerTransform.position, nodes[i].position);
            if (dist < nearestNodeADistance)
            {
                nearestNodeADistance = dist;
                nearestNodeA = nodes[i];
            }
        }
        for (int i = 0; i < nodes.Length; i++)
        {
            float dist = Vector3.Distance(_listenerTransform.position, nodes[i].position);
            if (dist < nearestNodeBDistance && dist > nearestNodeADistance)
            {
                nearestNodeBDistance = dist;
                nearestNodeB = nodes[i];
            }
        }
        float distanceBetweenNodes = Vector3.Distance(nearestNodeA.position, nearestNodeB.position);
        float volumeA = nearestNodeA.volume * (1-(nearestNodeADistance/distanceBetweenNodes));
        float volumeB = nearestNodeB.volume * (1-(nearestNodeBDistance/distanceBetweenNodes));
        float totalVolume = Mathf.Clamp01(volumeA + volumeB);
        _audioSource.volume = _falloffCurve.Evaluate(totalVolume);
    }
    void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
        foreach(AudioNode node in nodes)
        {
            // Draw a sphere at the nodes' position
            Gizmos.color = new Color(node.volume,0f,0f,1f);
            Gizmos.DrawSphere(node.position, 0.25f);
            Handles.Label(node.position + new Vector3(0,0.5f,0), (node.volume * 100).ToString() + "%");
        }
        if (_listenerTransform != null)
        {
            // Draw a sphere at the listener's position
            Gizmos.color = new Color(_audioSource.volume,0f,1f,1f);
            Gizmos.DrawSphere(_listenerTransform.position, 0.25f);
            Handles.Label(_listenerTransform.position + new Vector3(0,0.5f,0), Mathf.RoundToInt(_audioSource.volume * 100).ToString() + "%");
        }
        #endif
    }

    //Get distance from listener to all audio sources
    //Multiply source's volume value by (distance to listener, divide by source's max range)
    //add all volumes together
    //mult = 1-clamp01(distanceToListener / sourceMaxRange);
    //(sourceAVolume * mult) + (sourceBVolume * mult)
}