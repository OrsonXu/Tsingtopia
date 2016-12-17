using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class MessageManager : BaseManager {
	
	//hold reference to events and events
	private Dictionary <string, UnityEvent> eventDictionary;

	private static MessageManager messageManager;


	public override void Awake(){
		Debug.Log("MessageTrigger.Awake");
	}

	// Make messageManager can be got global easily
	public static MessageManager instance	{
		get	{
			if (!messageManager){
				messageManager = FindObjectOfType (typeof (MessageManager)) as MessageManager;
				if (!messageManager){
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else{
					messageManager.Init (); 
				}
			}
			return messageManager;
		}
	}

	// Initialize messageManager
	void Init (){
		if (eventDictionary == null){
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

	// Start listener
	public static void StartListening (string eventName, UnityAction listener){
		UnityEvent thisEvent = null;
        Debug.Log("MessageManager adds " + eventName);
		// Try get value a eventName and add listener to it
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)){
			thisEvent.AddListener (listener);
		} 
		else{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener){
		if (messageManager == null) return;
		UnityEvent thisEvent = null;
		// Try to get unity event by event name and remove its listener
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)){
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName){
		Debug.Log ("MessageManager tries to trigger " + eventName);
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)){
			thisEvent.Invoke ();
            Debug.Log("MessageManager successfully triggers " + eventName);
		}
	}
}