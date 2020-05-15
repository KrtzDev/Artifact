using UnityEngine;

public class LineRendererGrapplingHook : MonoBehaviour
{
    public Transform origin;
    public Transform weapon;
    public LineRenderer lineRenderer;

    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, weapon.position);
    }
}
