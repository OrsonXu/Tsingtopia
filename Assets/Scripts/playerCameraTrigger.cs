using UnityEngine;
using System.Collections;

public class playerCameraTrigger : MonoBehaviour {

	void Awake(){
		Debug.Log("MessageTrigger.Awake");
	}

	void Update () {
		if (Input.GetKeyDown ("L")){
			Debug.Log ("Trigger pressed");
			MessageManager.TriggerEvent ("PlayerDisableMovement");
		}

	}
}
