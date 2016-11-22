using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float Speed { get; set; }

    private Rigidbody playerRigidbody;
    private Vector3 movement;
    private float h;
    private float v;
    private Ray camRay;
    private RaycastHit floorHit;
    private float camRayLength = 100f;
    private int floorMask;
    private Vector3 playerToMouse;
    private Quaternion newRotation;

    Vector3 moveDirection;
    bool rightMouseActive;
    RaycastHit rightMouseRay = new RaycastHit();
    Vector3 rightMouseTarget;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");

        rightMouseTarget = transform.position;
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Move(h, v);

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
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;
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
            playerRigidbody.MovePosition(transform.position + moveDirection.normalized * Speed * Time.deltaTime);
        }

    }

    void Turning()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;

            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;
            newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

}
