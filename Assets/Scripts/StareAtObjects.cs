using System;
using Ghoulish.InteractionSystem;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem.XR;

public class StareAtObjects : MonoBehaviour
{
    private Vector3 _currentLookPos;
    private float _globalInfluence = 0f; 
    [SerializeField] private Transform _headBone;
    [SerializeField] private float _interactRange = 2f;
    [SerializeField] private LayerMask _interactLayerMask = default;
    [SerializeField] private StaringBone[] staringBones;
    
    [Serializable]
    private class StaringBone
    {
        public Transform bone;
        [Tooltip("Maximum local rotation range allowed, from original rotation")]
        public Vector3 clamp;
        [Tooltip("How much will this bone rotate to look at the target")]
        [Range(0f,1f)]
        public float influence;
        [Tooltip("How many seconds per frame this bone rotates")]
        public float speed;
        [HideInInspector] public Quaternion originalLocalRotation;
        [HideInInspector] public Quaternion storedQuaternion;
    }

    void Start()
    {
        foreach(StaringBone b in staringBones)
        {
            b.originalLocalRotation = b.bone.localRotation;
        }
    }
    void LateUpdate()
    {
        Transform nearestTarget = GetNearestTarget();
        if (nearestTarget == null)
        {
            _globalInfluence = Mathf.MoveTowards(_globalInfluence, 0f, 0.5f * Time.deltaTime);
        }
        else
        {
            _globalInfluence = 1f;
            _currentLookPos = nearestTarget.position;
        }
        StareAtTarget(_currentLookPos);  
    }
    private Transform GetNearestTarget()
    {
        Vector3 colliderPosition = transform.position + (transform.forward * 0.5f); //move collider position in front of player for more natural interaction
        Collider[] colliderArray = Physics.OverlapSphere(colliderPosition, _interactRange, _interactLayerMask);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out StareTarget _))
            {
                return collider.transform;
            }
        }
        return null;
    }

    private void StareAtTarget(Vector3 target)
    {
        foreach(StaringBone b in staringBones)
        {
            //store the animator's bone changes
            Quaternion animatedBoneRot = b.bone.rotation;
            //restore the stored rotations to counteract the animator's changes
            b.bone.rotation = b.storedQuaternion;
            
            //Get the direction to the target as a worldspace rotation
            Vector3 targetDirection = (target - b.bone.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            //Weight how much the bone's worldspace rotation should be influenced
            Quaternion weightedRotation = Quaternion.Slerp(animatedBoneRot, targetRotation, b.influence);
            //Smoothly rotate toward the desired worldspace rotation
            weightedRotation = Quaternion.RotateTowards(b.bone.rotation, weightedRotation, b.speed);
            //Clamp the bone's local euler angle - why was this so hard!
            Vector3 worldEuler = weightedRotation.eulerAngles;
            Vector3 originalWorldEuler = (b.bone.parent.rotation * b.originalLocalRotation).eulerAngles;
            worldEuler.x = ClampAngle(worldEuler.x, originalWorldEuler.x - b.clamp.x, originalWorldEuler.x + b.clamp.x);
            worldEuler.y = ClampAngle(worldEuler.y, originalWorldEuler.y - b.clamp.y, originalWorldEuler.y + b.clamp.y);
            worldEuler.z = ClampAngle(worldEuler.z, originalWorldEuler.z - b.clamp.z, originalWorldEuler.z + b.clamp.z);

            //Mix between animated bone pos and this bone pose based on Global Influence
            Vector3 globalInfluencedEuler = Quaternion.Slerp(animatedBoneRot, Quaternion.Euler(worldEuler), _globalInfluence).eulerAngles;
            b.bone.eulerAngles = globalInfluencedEuler;

            //Store the changed rotation because the animator is going to override it next frame
            b.storedQuaternion = b.bone.rotation;
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        return Mathf.Clamp(angle, min + floor, max + floor);
    }

    //This function works but NOT in combination with animated bones...
    public static Vector3 SmoothDampEuler(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
    {
        return new Vector3(
            Mathf.SmoothDampAngle(current.x, target.x, ref currentVelocity.x, smoothTime),
            Mathf.SmoothDampAngle(current.y, target.y, ref currentVelocity.y, smoothTime),
            Mathf.SmoothDampAngle(current.z, target.z, ref currentVelocity.z, smoothTime)
        );
    }
}