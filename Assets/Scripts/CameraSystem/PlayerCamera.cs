using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    public Transform target;
    public float smoothing;
	public KeyCode leftRotate = KeyCode.O;
	public KeyCode rightRotate = KeyCode.P;
    
    private Vector3 offset;
    private Vector3 fixedOffset;
    private float distance;
    private float fixedDistance;
    private float minDistance;
    private float maxDistance;
    private float scrollSpeed;

	private float _rotateAngel = 5f;



	private void OnEnable(){
		MessageManager.TriggerEvent ("PlayerEnableMovement");
		// Ensure the cursor is not locked and visible
		Screen.lockCursor = false;
		Cursor.visible = true;
	}


	private void OnDisable(){
		MessageManager.TriggerEvent ("PlayerDisableMovement");

	}
    void Start() {
        // Set offsets
        fixedOffset = -target.position + transform.position;
        offset = - target.position + transform.position;
        // Set scroll speed
        scrollSpeed = 30f;
    }

    void FixedUpdate()
    {
        // Zoom view using mouse 
        ScrollView();
        //Camera = getComponents<Camera>();
        Vector3 Camposition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, Camposition, smoothing * Time.deltaTime);  
		Rotate ();

    }

	private void ScrollView()
	{
		fixedDistance = fixedOffset.magnitude;
		distance = offset.magnitude;
		distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
		distance = Mathf.Clamp(distance, 0.5f * fixedDistance, 3.0f * fixedDistance);
		offset = offset.normalized * distance;
	}

	private void Rotate(){
		if(Input.GetKey(leftRotate))
			transform.RotateAround (target.position, Vector3.up, _rotateAngel);
		if(Input.GetKey(rightRotate))
			transform.RotateAround (target.position, Vector3.up, -1f * _rotateAngel);
		fixedOffset = -target.position + transform.position;
        //offset = - target.position + transform.position;
	}
    //void Turning()
    //{
        
    //    // Create a ray from the mouse cursor on screen in the direction of the camera.
    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    // Create a RaycastHit variable to store information about what was hit by the ray.
    //    RaycastHit floorHit;

    //    // Perform the raycast and if it hits something on the floor layer...
    //    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
    //    {
    //        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
    //        Vector3 playerToMouse = floorHit.point - transform.position;

    //        // Ensure the vector is entirely along the floor plane.
    //        playerToMouse.y = 0f;

    //        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
    //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

    //        // Set the player's rotation to this new rotation.
    //        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);


    //    }
    //}

  
    
}
