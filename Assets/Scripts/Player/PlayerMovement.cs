using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float Speed { get; set; }

    private Rigidbody rb;
    private Vector3 movement;
    private float h;
    private float v;
    private Ray camRay;
    private RaycastHit floorHit;
    private float camRayLength = 100f;
    private int floorMask;
    private Vector3 playerToMouse;
    private Quaternion newRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * Speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }
}
