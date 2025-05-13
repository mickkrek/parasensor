using UnityEngine;

public class AttachLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform target;

    void OnEnable()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount-1, target.transform.position);
    }
}
