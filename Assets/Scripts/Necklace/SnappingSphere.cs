using UnityEngine;

public class SnappingSphere : MonoBehaviour
{
    public Renderer sphereRenderer;

    void Start()
    {
        // Make the sphere initially invisible
        sphereRenderer.enabled = false;
    }

    public void Show()
    {
        // Make the snapping sphere visible
        sphereRenderer.enabled = true;
    }

    public void Hide()
    {
        // Make the snapping sphere invisible
        sphereRenderer.enabled = false;
    }
}
