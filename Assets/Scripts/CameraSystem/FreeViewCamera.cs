using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FreeViewCamera : MonoBehaviour{
	public float initialSpeed = 10f;
	public float increaseSpeed = 1.25f;

	public bool allowMovement = true;
	public bool allowRotation = true;

	public KeyCode forwardButton = KeyCode.W;
	public KeyCode backwardButton = KeyCode.S;
	public KeyCode rightButton = KeyCode.D;
	public KeyCode leftButton = KeyCode.A;

	public float cursorSensitivity = 0.025f;
	public bool cursorToggleAllowed = true;
	public KeyCode cursorToggleButton = KeyCode.Escape;

	private float currentSpeed = 0f;
	private bool moving = false;
	private bool togglePressed = false;

	// Overwrite Unity function, update when input exist
	private void Update(){
		
		if (allowMovement){
			Move ();
		}

		if (allowRotation){
			Rotate ();
		}

		if (cursorToggleAllowed){
			Toggle ();
		}
		else{
			togglePressed = false;
			Cursor.visible = false;
		}
	}

	// Overwrite Unity funciton, register in Message Manager
	private void OnEnable(){
		MessageManager.TriggerEvent ("PlayerDisableMovement");
		Debug.Log ("Free view Camera Enabled");
		if (cursorToggleAllowed){
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
	}
	// Overwrite the Unity function, unregister event in Message Manager and free the cursor
	private void OnDisabled(){
		MessageManager.TriggerEvent ("PlayerEnableMovement");
		Debug.Log ("Free view Camera Disabled");
		Screen.lockCursor = false;
		Cursor.visible = true;
	}
	// Response to keyboard input to move the tranform of camera
	private void Move(){
		bool lastMoving = moving;
		Vector3 deltaPosition = Vector3.zero;
		if (moving)
			currentSpeed += increaseSpeed * Time.deltaTime;
		moving = false;

		CheckMove(forwardButton, ref deltaPosition, transform.forward);
		CheckMove(backwardButton, ref deltaPosition, -transform.forward);
		CheckMove(rightButton, ref deltaPosition, transform.right);
		CheckMove(leftButton, ref deltaPosition, -transform.right);

		if (moving){  
			if (moving != lastMoving)
				currentSpeed = initialSpeed;
			transform.position += deltaPosition * currentSpeed * Time.deltaTime;
		}
		else currentSpeed = 0f;         
	}
	// Rotate the camera when mouse moves
	private void Rotate(){
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.x += -Input.GetAxis("Mouse Y") * 359f * cursorSensitivity;
		eulerAngles.y += Input.GetAxis("Mouse X") * 359f * cursorSensitivity;
		transform.eulerAngles = eulerAngles;
	}
	// if toggled, stop camera movement
	private void Toggle(){

		if (Input.GetKey(cursorToggleButton)){
			if (!togglePressed)	{
				togglePressed = true;
				Screen.lockCursor = !Screen.lockCursor;
				Cursor.visible = !Cursor.visible;
				allowMovement = !allowMovement;
				allowRotation = !allowRotation;
			}
		}
		else togglePressed = false;
	}
	// checkMove move camera response to a key input
	private void CheckMove(KeyCode keyCode, ref Vector3 deltaPosition, Vector3 directionVector){
		if (Input.GetKey(keyCode)){
			moving = true;
			deltaPosition += directionVector;
		}
	}
}
