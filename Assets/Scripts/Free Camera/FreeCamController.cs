using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamController : MonoBehaviour
{
    public static Camera FreeCamera;
    public float sensitivity = 5;
    public float normalSpeed = 15;
    public float sprintSpeed = 25;
    float currentSpeed;

    void Start()
    {
        FreeCamera = GameObject.Find("FreeCamPlayer").GetComponent<Camera>();
    }

    void Update()
    {
        if (DeerOOP.FreeCamControllerUpdater || WolfOOP.FreeCamControllerUpdater)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Movement();
            Rotation();
        }
    }

    public void Rotation()
    {
        // Mouse movement input
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement()
    {
        // Horizontal and vertical input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // Adjust Y-axis movement
        if (Input.GetKey(KeyCode.Space))
        {
            input.y = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            input.y = -1f;
        }

        // Adjust Z-axis movement
        if (Input.GetKey(KeyCode.W))
        {
            input.z = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            input.z = -1f;
        }

        // Adjust X-axis movement
        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            input.x = 1f;
        }

        // Checks if speed is sprint or normal
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }
        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}



