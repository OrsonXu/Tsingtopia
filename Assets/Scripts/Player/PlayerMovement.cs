//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 6f;

//    private Rigidbody rb;
//    private Animator anim;
//    private Vector3 movement;
//    private float h;
//    private float v;
//    private Ray camRay;
//    private RaycastHit floorHit;
//    private float camRayLength = 100f;
//    private int floorMask;
//    private Vector3 playerToMouse;
//    private Quaternion newRotation;

//    void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//        anim = GetComponent<Animator>();
//        floorMask = LayerMask.GetMask("Floor");
//    }

//    void FixedUpdate()
//    {
//        h = Input.GetAxisRaw("Horizontal");
//        v = Input.GetAxisRaw("Vertical");
//        Move(h, v);
//        Turning();
//        Animating(h, v);
//    }

//    void Move(float h, float v)
//    {
//        movement.Set(h, 0f, v);
//        movement = movement.normalized * speed * Time.deltaTime;
//        rb.MovePosition(transform.position + movement);
//    }

//    void Turning()
//    {
//        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
//        {
//            playerToMouse = floorHit.point - transform.position;
//            playerToMouse.y = 0f;
//            newRotation = Quaternion.LookRotation(playerToMouse);
//            rb.MoveRotation(newRotation);
//        }
//    }

//    void Animating(float h, float v)
//    {
//        bool walking = false;
//        if (h != 0 || v != 0)
//        {
//            walking = true;
//        }
//        anim.SetBool("IsWalking", walking);
//    }
//}






using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    Vector3 moveDirection;
    bool rightMouseActive;
    RaycastHit rightMouseRay = new RaycastHit();
    Vector3 rightMouseTarget;
    Vector3 playerToMouse;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        rightMouseTarget = transform.position;
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        rightMouseActive = (bool)Input.GetButton("Fire2");
        // Move the player around the scene.
        if(h != 0 || v != 0)
        {
            Move(h, v);
            rightMouseActive = false;
        }

        //MoveToward();

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    

    void MoveToward()
    {
        if (rightMouseActive)
        {
            if (Input.GetButton("Fire2"))
            {
                rightMouseTarget = playerToMouse;
            }
           
        }
        moveDirection = rightMouseTarget - transform.position;
        moveDirection.y = 0f;
        if (moveDirection.magnitude > 10)
        {
            moveDirection = moveDirection.normalized;
            playerRigidbody.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
        }

    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);

            
        }
    }
    
    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
