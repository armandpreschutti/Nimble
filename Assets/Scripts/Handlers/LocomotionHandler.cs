
using JetBrains.Annotations;
using UnityEngine;



public class LocomotionHandler : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public Animator anim;
    public LayerMask groundLayer; // Specify the ground layer in the Inspector

    [Header("Input")]
    public float horizontal;
    public float vertical;
    public Vector3 camForward;
    public Vector3 camRight;

    [Header("Attributes")]
    public float rotationSpeed = 2f;
    public Vector3 directionRelative;
    public float jumpPower;
    public float startSpeed;
    public float maxSpeed;
    public float acceleration; // Interpolating halfway between startValue and endValue
    public float speed;
    public bool grounded;

    [Header("Debug")]
    public float debugMagnitude;
    public float debugInputMagnitude;
    public Vector3 debugVelocity;
    public Vector3 debugNormalized;
    public Vector3 debugLean;
    public Quaternion result;
    

    private void Update()
    {
        PlayerInput();
        CameraInput();
        // Apply the velocity to the Rigidbody using AddForce
        Animate();
        Jump();
        
    }

    private void FixedUpdate()
    {

        GroundCheck();
        Movement();
        
        debugInputMagnitude = new Vector3(horizontal, 0, vertical).normalized.magnitude;
        debugMagnitude = rb.velocity.magnitude;
        debugVelocity = rb.velocity;
        debugNormalized = rb.velocity.normalized;
    }

    

    private void PlayerInput()
    {
        // Get horizontal input 
        horizontal = Input.GetAxis("Horizontal");

        // Get vertical input
        vertical = Input.GetAxis("Vertical");
    }

    private void CameraInput()
    {
        // Get forward vector of camera
        camForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

        // Get right vector of camera
        camRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
    }
    
    private void Movement()
    {
        // Create forward vector relative to player
        Vector3 forwardRelative = vertical * camForward;

        // Create right vector relative to player
        Vector3 rightRelative = horizontal * camRight;

        // Combine forward and right vector and normalize
        directionRelative = (forwardRelative + rightRelative).normalized;
        

        // Calculate the desired velocity based on speed
        Vector3 targetVelocity = new Vector3(directionRelative.x * speed, rb.velocity.y, directionRelative.z * speed);

        //rb.velocity = targetVelocity;
       //rb.velocity =  Vector3.Lerp(rb.position, targetVelocity * maxSpeed, acceleration);
        // Calculate the change in velocity (acceleration) needed to reach the target velocity
        Vector3 accelerationVector = (targetVelocity - rb.velocity) * acceleration;

        // Apply the calculated acceleration to the Rigidbody
        rb.AddForce(accelerationVector, ForceMode.Acceleration);

        // Optional: Cap the maximum speed to avoid excessive acceleration
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (grounded)
        {
            
        }
        else
        {
            return;
        }
        
    }
   
    public void Animate()
    {
        if(horizontal != 0 || vertical != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);

        }
        if (grounded)
        {
            anim.SetBool("isFalling", false);
        }
        else
        {
            anim.SetBool("isFalling", true);
        }
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Fire1") && grounded)
        {
            rb.AddForce(Vector3.up * jumpPower);
        }
        else
        {
            return;
        }
    }

    

    public void GroundCheck()
    {
        // Set the length of the raycast
        float raycastLength = 1.0f; // Adjust this value based on your object's size
        
        // Cast a downward ray from the object's position
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y +1, transform.position.z), Vector3.down);

        RaycastHit hit;

        // Perform the raycast and check if it hits the ground layer
        if (Physics.Raycast(ray, out hit, raycastLength, groundLayer))
        {

            Debug.DrawLine(ray.origin, hit.point, Color.green);
            // A collision with the ground layer has occurred
            grounded =true;
        }
        else
        {
            grounded= false;
        }
    }
}



