using UnityEngine;

public class IKHandSnapping : MonoBehaviour
{
    [SerializeField] private Transform _distanceCenter, _handBone;
    [SerializeField] private float _radius = 2f, _smoothSpeed = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector3 _SnappedRotationOffset = new Vector3(-90f,0f,0f);
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private float _globalInfluence, _targetInfluence;

    public void LateUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_handBone.position, _radius, _layerMask);
        if (hitColliders.Length == 0)
        {
            _targetInfluence = 0f;
        }
        else
        {
            _targetInfluence = 1f;
            foreach (var hitCollider in hitColliders)
            {
                Vector3 thisClosestPoint = hitCollider.ClosestPoint(_distanceCenter.position);
                if (Vector3.Distance(thisClosestPoint, _distanceCenter.position) < Vector3.Distance(_targetPosition, _distanceCenter.position))
                {
                    _targetPosition = thisClosestPoint;
                    _targetRotation = _handBone.rotation * Quaternion.Euler(_SnappedRotationOffset);
                }
            }
        }
        _globalInfluence = Mathf.MoveTowards(_globalInfluence, _targetInfluence, _smoothSpeed * Time.deltaTime);
        Vector3 lerpedPos = Vector3.Lerp(_handBone.position, _targetPosition, _globalInfluence);
        Quaternion lerpedRot = Quaternion.Slerp(_handBone.rotation, _targetRotation, _globalInfluence);
        transform.SetPositionAndRotation(lerpedPos, lerpedRot);
    }
}
