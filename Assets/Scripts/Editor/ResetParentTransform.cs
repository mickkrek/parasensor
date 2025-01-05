using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEditor;
using UnityEngine;

public class ResetParentTransform : EditorWindow
{
    private static Transform _activeTransform;

    private static Vector3[] _childPositions;

    [MenuItem("Tools/Reset Parent Transform/Reset Position")]
    public static void ResetPositionApply()
    {
        GetActiveTransform();
        ResetPosition();
    }

    public static void GetActiveTransform()
    {
        _activeTransform = Selection.activeTransform;
    }

    public static void ResetPosition()
    {
        Vector3 averagePos = Vector3.zero;

        Transform[] _childTransforms = _activeTransform.GetComponentsInChildren<Transform>();
        _childPositions = new Vector3[_childTransforms.Length];
        for(int i = 0; i < _childTransforms.Length; i++) 
        {
            if(_childTransforms[i] != _activeTransform)
            {
                _childPositions[i] = _childTransforms[i].position;
                averagePos += _childPositions[i];
            }
        }
        averagePos /= _childPositions.Length;
        _activeTransform.position = averagePos;
        for(int i = 0; i < _childTransforms.Length; i++) 
        {
            if(_childTransforms[i] != _activeTransform)
            {
                _childTransforms[i].position = _childPositions[i];
            }
        }
    }
}