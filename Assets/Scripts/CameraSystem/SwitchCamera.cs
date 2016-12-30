using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

	public Camera[] CameraList;
	public bool allowSwitch = true;
	public KeyCode switchBotton = KeyCode.U;

	//private
	private int _activatedCamera = 0;
	//private float _lastFrameTime;
	private float _leastResponseTime = 0.1f;
	private float _lastResponseTime;


	// Initialize the camera switcher 
	private void Start(){
		_lastResponseTime = 0f;

		for (int count = 0; count < CameraList.Length; count++) {
			CameraList [count].gameObject.SetActive(false);
		}
		CameraList [_activatedCamera].gameObject.SetActive(true);
		_activatedCamera += 1;
	}
	// Switch camera in camera list
	private void Switch(){
		for (int count = 0; count < CameraList.Length; count++) {
			if (count == _activatedCamera)
				CameraList [count].gameObject.SetActive(true);
			else
				CameraList [count].gameObject.SetActive(false);
		}
		_activatedCamera += 1;
		if(_activatedCamera >= CameraList.Length)
			_activatedCamera = 0;
	}
	// Update the switch if allow switch
	private void Update(){
		if (_lastResponseTime < _leastResponseTime) {
			_lastResponseTime += Time.deltaTime;
		}
		else {
			_lastResponseTime = 0f;
			if(allowSwitch){
				//Debug.Log ("switchCamera onuse");
				if (Input.GetKey (switchBotton)){
					//Debug.Log ("switchBotton Pressed");
					Switch ();
				}
			}
			//Debug.Log ("_activatedCamera" + _activatedCamera.ToString ());
		}
	}
}
