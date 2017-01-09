using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//[HideInInspector]
	public float initialSpeed = 10f;
	public float increaseSpeed = 1.25f;
	public float maxSpeed = 0f;

	// Allow move FLAG
	public bool allowMovement = true;

	// Keyboard bound key settings
	public KeyCode forwardButton = KeyCode.W;
	public KeyCode backwardButton = KeyCode.S;
	public KeyCode rightButton = KeyCode.D;
	public KeyCode leftButton = KeyCode.A;


	//*******//
	//Speed and moving FLAG
	private float _currentSpeed = 0f;
	private bool _moving = false;

	private Rigidbody _playerRigidbody;
	private Vector3 _movement;

	private Ray _camRay;
	private RaycastHit _floorHit;
	private float _camRayLength = 1000f;
	private int _floorMask;

	private Vector3 _playerToMouse;
	private Quaternion _newRotation;

	private Camera _playerCamera;

    private Vector3 moveDirection;
    private Vector3 rightMouseTarget;
    private bool rightMouseActive;
    private RaycastHit rightMouseRay = new RaycastHit();
    /// <summary>
    /// Override, register a message event
    /// </summary>
	private void OnEnable(){
		MessageManager.StartListening("PlayerEnableMovement", AllowMovement);
		MessageManager.StartListening("PlayerDisableMovement", DisableMovement);
	}
    /// <summary>
    /// Override, unregister a message event
    /// </summary>
    private void OnDisable()
    {
		MessageManager.StopListening("PlayerEnableMovement", AllowMovement);
		MessageManager.StopListening("PlayerDisableMovement", DisableMovement);
	}

	void AllowMovement(){
		allowMovement = true;
	}
	void DisableMovement(){
		allowMovement = false;
	}
	void Awake()
	{
		_playerRigidbody = GetComponent<Rigidbody>();
		_floorMask = LayerMask.GetMask("Floor");
		rightMouseTarget = transform.position;
		GameObject playerCameraGO = GameObject.FindWithTag ("PlayerCamera");
		_playerCamera = playerCameraGO.GetComponent<Camera>();
	}

	private void FixedUpdate()
	{
		// if allowed to move, move
		if (allowMovement){
			Move ();
		}
		// turn to a direction based on mouse cursor
		Turning();
	}


	private void Move(){
		bool lastMoving = _moving;
		Vector3 deltaPosition = Vector3.zero;
		if (_moving && (_currentSpeed <= initialSpeed + maxSpeed))
			_currentSpeed += increaseSpeed * Time.deltaTime;
		_moving = false;
        // Check the move input
		CheckMove(forwardButton, ref deltaPosition, Vector3.forward);
        CheckMove(backwardButton, ref deltaPosition, -Vector3.forward);
        CheckMove(rightButton, ref deltaPosition, Vector3.right);
        CheckMove(leftButton, ref deltaPosition, -Vector3.right);
        // If can move
		if (_moving){  
			if (_moving != lastMoving)
				_currentSpeed = initialSpeed;
            //transform.position += deltaPosition * _currentSpeed * Time.deltaTime;
            Vector3 movement = deltaPosition.normalized * _currentSpeed * Time.deltaTime;
            //movement = movement.normalized;
            _playerRigidbody.MovePosition(transform.position + movement);
		}
		else _currentSpeed = 0f;         
	}

	private void CheckMove(KeyCode keyCode, ref Vector3 deltaPosition, Vector3 directionVector){
		if (Input.GetKey(keyCode)){
			_moving = true;
			deltaPosition += directionVector;
		}
	}

	void Turning()
	{
		_camRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(_camRay, out _floorHit, _camRayLength, _floorMask))
		{
            //Debug.Log("Camera Ray Hit somewhere");
			_playerToMouse = _floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			_playerToMouse.y = 0f;
			_newRotation = Quaternion.LookRotation(_playerToMouse);
			_playerRigidbody.MoveRotation(_newRotation);
		}
	}

}
