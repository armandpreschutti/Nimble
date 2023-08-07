using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public LocomotionHandler locomotion;
    public Rigidbody rb;
    public float rotationSpeed;
    public Vector3 rotateDirection;
    public Vector3 tiltDirection;
    public float tiltAngle;
    public float tiltSpeed;
    public Vector3 result;
    public Vector3 angle;
    public GameObject testObject;
    public Vector3 rotationVector;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
        Rotate();
    }

    private void Rotate()
    {
        // Get direction to rotate towards
        rotateDirection = new Vector3(locomotion.directionRelative.x, 0, locomotion.directionRelative.z);

        // Check if the direction is not zero (to avoid NaN errors)
        if (rotateDirection != Vector3.zero)
        {
            // Calculate the rotation angle based on the velocity direction
            Quaternion targetRotation = Quaternion.LookRotation(rotateDirection);

            // Smoothly rotate the object towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (locomotion.grounded)
        {
            
        }
        else
        {
            return;
        }
        
    }
    public void Tilt()
    {

        rotationVector = new Vector3(locomotion.directionRelative.z, transform.rotation.y, -locomotion.directionRelative.x) * tiltAngle;
        //Vector3 rotationVector = new Vector3(rb.velocity.z, transform.rotation.y, rb.velocity.x) * tiltAngle;
        Quaternion tiltRotation = Quaternion.Euler(rotationVector);
        //transform.rotation = tiltRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, tiltSpeed * Time.deltaTime);

    }
}
