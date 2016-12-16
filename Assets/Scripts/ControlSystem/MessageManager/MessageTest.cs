using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MessageTest : MonoBehaviour {

	private UnityAction someListener;

	void Awake (){
		someListener = new UnityAction (SomeFunction);
	}

	void OnEnable (){
		MessageManager.StartListening ("test", someListener);
		MessageManager.StartListening ("Spawn", SomeOtherFunction);
		MessageManager.StartListening ("Destroy", SomeThirdFunction);
	}

	void OnDisable (){
		MessageManager.StopListening ("test", someListener);
		MessageManager.StopListening ("Spawn", SomeOtherFunction);
		MessageManager.StopListening ("Destroy", SomeThirdFunction);
	}

	void SomeFunction (){
		Debug.Log ("Some Function was called!");
	}

	void SomeOtherFunction (){
		Debug.Log ("Some Other Function was called!");
	}

	void SomeThirdFunction (){
		Debug.Log ("Some Third Function was called!");
	}
}