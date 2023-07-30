using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LocomotionHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float speed;
    public Vector3 directionRelative;
    public Rigidbody rb;
    public float rotationSpeed = 2f;
    

    public void Update()
    {

        PlayerInput();      
        
        
    }
    public void FixedUpdate()
    {
        rb.velocity = directionRelative * speed;
        Rotate();
    }
    private void Rotate()
    {
        // Get the velocity of the rigidbody
        Vector3 velocity = rb.velocity;

        // Check if the velocity is not zero (to avoid NaN errors)
        if (velocity != Vector3.zero)
        {
            // Calculate the rotation angle based on the velocity direction
            Quaternion targetRotation = Quaternion.LookRotation(velocity);

            // Smoothly rotate the object towards the target rotation
            rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camRight.y = 0;
        camForward.y = 0;
        Vector3 forwardRelative = vertical * camForward;
        Vector3 rightRelative = horizontal * camRight;

        directionRelative = forwardRelative + rightRelative;
    }
}
