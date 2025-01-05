using System;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEditor;
using UnityEngine;

public class BakeSpriteToMesh : EditorWindow
{
    private static Transform _activeTransform;

    [MenuItem("Tools/Bake Sprite To Mesh")]
    public static void ResetPositionApply()
    {
        GetActiveTransform();
        if (_activeTransform != null)
        {
            GenerateMesh();
        }
    }

    public static void GetActiveTransform()
    {
        _activeTransform = Selection.activeTransform;
    }

    public static void GenerateMesh()
    {
        GameObject newObject = new GameObject(_activeTransform.name + "_BAKEDSPRITE");

        Debug.Log("Commencing Shrink Wrap Ocean...");

        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        //meshRenderer.sharedMaterial = _mat;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        
        SpriteRenderer spriteRend = _activeTransform.GetComponent<SpriteRenderer>();

        Mesh mesh = new Mesh
        {
            vertices = Array.ConvertAll(spriteRend.sprite.vertices, i => (Vector3)i),
            uv = spriteRend.sprite.uv,
            triangles = Array.ConvertAll(spriteRend.sprite.triangles, i => (int)i),
            name = _activeTransform.name + "_BAKEDSPRITE [" + _activeTransform.GetInstanceID() + "]"
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
        newObject.transform.SetParent(_activeTransform);
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localRotation = Quaternion.Euler(0,0,0);
        newObject.transform.localScale = Vector3.one;
        newObject.isStatic = true;
        spriteRend.enabled = false;
    }
}