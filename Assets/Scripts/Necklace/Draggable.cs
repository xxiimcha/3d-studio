using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    public static Draggable currentlyDraggedPrefab; // Store the currently dragged prefab
    private SnappingSphere currentSnappingSphere; // Reference to the current snapping sphere

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if this instance is the currently dragged one
        if (currentlyDraggedPrefab == this)
        {
            // Update position of the prefab while dragging
            transform.position = GetMouseWorldPosition() + offset;

            // Continuously check for snapping sphere under mouse
            ShowSnappingSphereUnderMouse();
        }

        // Check for mouse up to stop dragging
        if (Input.GetMouseButtonUp(0) && currentlyDraggedPrefab == this)
        {
            currentlyDraggedPrefab = null; // Reset the currently dragged prefab

            // Snap to the current snapping sphere if it's valid
            if (currentSnappingSphere != null)
            {
                SnapToSphere(currentSnappingSphere);
            }

            // Reset the current snapping sphere
            currentSnappingSphere = null;
        }
    }

    private void OnMouseDown()
    {
        currentlyDraggedPrefab = this; // Set this instance as the currently dragged prefab
        offset = transform.position - GetMouseWorldPosition(); // Calculate the offset for dragging
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // Return the hit point on the ground or other objects
        }
        return Vector3.zero; // Fallback to zero if nothing is hit
    }

    private void ShowSnappingSphereUnderMouse()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SnappingSphere snappingSphere = hit.collider.GetComponent<SnappingSphere>();
            if (snappingSphere != null)
            {
                currentSnappingSphere = snappingSphere; // Keep track of the current snapping sphere
                snappingSphere.Show(); // Show the snapping sphere when mouse is over it
            }
            else
            {
                // Hide all snapping spheres if mouse is not over any
                HideAllSnappingSpheres();
                currentSnappingSphere = null; // Reset current snapping sphere if not over one
            }
        }
        else
        {
            // Hide all snapping spheres if the raycast hits nothing
            HideAllSnappingSpheres();
            currentSnappingSphere = null; // Reset current snapping sphere if not over one
        }
    }

    private void SnapToSphere(SnappingSphere snappingSphere)
    {
        // Set the prefab's position to the snapping sphere's position
        transform.position = snappingSphere.transform.position;

        // Make the prefab a child of the snapping sphere
        transform.SetParent(snappingSphere.transform);

        snappingSphere.Hide(); // Optionally hide the sphere after snapping
    }

    private void HideAllSnappingSpheres()
    {
        // Hide all snapping spheres in the scene
        foreach (var sphere in FindObjectsOfType<SnappingSphere>())
        {
            sphere.Hide();
        }
    }
}
