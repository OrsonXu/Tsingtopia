using UnityEngine;
using System.Collections;

public class MessageTriggerTest : BaseManager {
	public override void Awake(){
		Debug.Log("MessageTrigger.Awake");
	}

	void Update () {
		if (Input.GetKeyDown ("q")){
			Debug.Log ("Trigger pressed");
			MessageManager.TriggerEvent ("test");
		}
		if (Input.GetKeyDown ("o")){
			MessageManager.TriggerEvent ("Spawn");
		}
		if (Input.GetKeyDown ("p")){
			MessageManager.TriggerEvent ("Destroy");
		}
		if (Input.GetKeyDown ("x")){	
			MessageManager.TriggerEvent ("Junk");
		}
	}
}