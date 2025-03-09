using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Define variables for movement type speeds and the mouse sensitivity
    public float moveSpeed = 5f; 
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;

    private float xRotation = 0f; 
    private CharacterController controller;
    private Camera playerCamera;

    private void Start()
    {
        // Define the organsim controllers and attach camera at start
        controller = GetComponent<CharacterController>(); 
        playerCamera = GetComponentInChildren<Camera>(); 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    private void Update()
    {
        // Handle mouse movement for camera look around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Limit the X axis rotation to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera on X-axis and Y-axis
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        transform.Rotate(Vector3.up * mouseX);

        // Handle movement (WASD keys)
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical"); 

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Sprint if shift is held down
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime); 

    }
}

