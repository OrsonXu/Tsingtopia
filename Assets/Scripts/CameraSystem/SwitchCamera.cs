using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

	public Camera[] CameraList;
	public bool allowSwitch = true;

	private int activatedCamera = 0;
	public KeyCode switchBotton = KeyCode.U;
	private void Start(){
		for (int count = 0; count < CameraList.Length; count++) {
			CameraList [count].enabled = false;
		}
		CameraList [activatedCamera].enabled = true;
		activatedCamera += 1;
	}
	private void Switch(){
		for (int count = 0; count < CameraList.Length; count++) {
			if (count == activatedCamera)
				CameraList [count].enabled = true;
			else
				CameraList [count].enabled = false;	
		}
		activatedCamera += 1;
		if(activatedCamera >= CameraList.Length)
			activatedCamera = 0;
	}
	private void Update(){
		
		if(allowSwitch){
			//Debug.Log ("switchCamera onuse");
			if (Input.GetKey (switchBotton)){
				//Debug.Log ("switchBotton Pressed");
				Switch ();
			}
		}
		//Debug.Log ("activatedCamera" + activatedCamera.ToString ());
	}
}
