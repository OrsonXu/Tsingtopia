using UnityEngine;
using System.Collections;

public class TaskTest : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			Debug.Log ("******Space pressed");
			TaskManager.TriggerTask("Task1");

		}
	}
}
