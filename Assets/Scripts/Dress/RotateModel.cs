using UnityEngine;
using UnityEngine.EventSystems;

public class RotateModel : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation
    public float smoothTime = 0.1f;    // Time it takes to smooth the rotation

    private float targetRotationY;
    private float currentRotationY;
    private float rotationVelocity;

    void Start()
    {
        // Initialize rotation values to match the starting rotation of the model
        currentRotationY = transform.localEulerAngles.y;
        targetRotationY = currentRotationY;
    }

    void Update()
    {
        // Check if the left mouse button is held down and not dragging any prefab
        if (Input.GetMouseButton(0) && !Draggable.currentlyDraggedPrefab && !IsPointerOverUI())
        {
            RotateModelWithMouse();
        }

        // Smoothly interpolate the rotation
        currentRotationY = Mathf.SmoothDampAngle(currentRotationY, targetRotationY, ref rotationVelocity, smoothTime);
        transform.localRotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    private void RotateModelWithMouse()
    {
        // Get the horizontal mouse movement
        float horizontalInput = Input.GetAxis("Mouse X");

        // Calculate the target rotation based on mouse movement and speed
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        targetRotationY -= rotationAmount;
    }

    private bool IsPointerOverUI()
    {
        // Check if the mouse pointer is over any UI element
        return EventSystem.current.IsPointerOverGameObject();
    }
}
