using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacterDepth : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private Transform _shadowCastersParent;
    [SerializeField] private Vector3 _offset;
    /*
    void Update()
    {
        Vector3 heading = transform.position - Camera.main.transform.position;
        float distance = Vector3.Dot(heading, Camera.main.transform.forward);

        gameObject.layer = LayerMask.NameToLayer(distance < _distance ? "Midground_3D" : "Background_3D");
        foreach(Transform child in GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.layer = LayerMask.NameToLayer(distance < _distance ? "Midground_3D" : "Background_3D");
        }
    }
    */
    void Awake()
    {
        if (_shadowCastersParent == null)
        {
            return;
        }
        foreach(Renderer rend in _shadowCastersParent.GetComponentsInChildren<Renderer>(true))
        {
            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << LayerMask.NameToLayer("MidgroundCollider");
        Vector3 target = transform.position + _offset;
        Vector3 heading = target - Camera.main.transform.position;
        heading = heading.normalized;
        float distance = Vector3.Distance(Camera.main.transform.position, target) - (_radius * 2f);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(Camera.main.transform.position, _radius, heading, out hit, distance, layerMask))
        {
            gameObject.layer = LayerMask.NameToLayer("Background_3D");
            foreach(Transform child in GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = LayerMask.NameToLayer("Background_3D");
            }
            if (_shadowCastersParent != null)
            {
                foreach(Transform child in _shadowCastersParent.GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Background_3D");
                }
            }
            Debug.DrawRay(Camera.main.transform.position, heading * distance, Color.yellow);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Midground_3D");
            foreach(Transform child in GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = LayerMask.NameToLayer("Midground_3D");
            }
            if (_shadowCastersParent != null)
            {
                foreach(Transform child in _shadowCastersParent.GetComponentsInChildren<Transform>(true))
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Midground_3D");
                }
            }
            
            Debug.DrawRay(Camera.main.transform.position, heading * 1000, Color.red);
        }
    }
}
