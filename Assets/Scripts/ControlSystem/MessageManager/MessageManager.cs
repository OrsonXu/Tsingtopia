using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// Instance the interface with param <bool>
[System.Serializable]
public class UnityEventBool : UnityEvent<bool>
{
}

// Instance the interface with param <int>
[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{
}

public class MessageManager : BaseManager {
	
	//hold reference to events and events
	private Dictionary <string, UnityEvent> eventDictionary;
    private Dictionary<string, UnityEventBool> eventDictionaryParamBool;
    private Dictionary<string, UnityEventInt> eventDictionaryParamInt;

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
        if (eventDictionaryParamBool == null)
        {
            eventDictionaryParamBool = new Dictionary<string, UnityEventBool>();
        }
        if (eventDictionaryParamInt == null)
        {
            eventDictionaryParamInt = new Dictionary<string, UnityEventInt>();
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
    public static void TriggerEvent(string eventName)
    {
        Debug.Log("MessageManager tries to trigger " + eventName);
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
            Debug.Log("MessageManager successfully triggers " + eventName);
        }
    }

    // Override with parameters <bool>
    public static void StartListening(string eventName, UnityAction<bool> listener)
    {
        UnityEventBool thisEvent = null;
        Debug.Log("MessageManager adds " + eventName);
        // Try get value a eventName and add listener to it
        if (instance.eventDictionaryParamBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEventBool();
            thisEvent.AddListener(listener);
            instance.eventDictionaryParamBool.Add(eventName, thisEvent);
        }
    }
    public static void StopListening(string eventName, UnityAction<bool> listener)
    {
        if (messageManager == null) return;
        UnityEventBool thisEvent = null;
        // Try to get unity event by event name and remove its listener
        if (instance.eventDictionaryParamBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void TriggerEvent(string eventName, bool flag)
    {
        Debug.Log("MessageManager tries to trigger " + eventName);
        UnityEventBool thisEvent = null;
        if (instance.eventDictionaryParamBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(flag);
            Debug.Log("MessageManager successfully triggers " + eventName);
        }
    }
    
    // Override with parameter <int>
    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        UnityEventInt thisEvent = null;
        Debug.Log("MessageManager adds " + eventName);
        // Try get value a eventName and add listener to it
        if (instance.eventDictionaryParamInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEventInt();
            thisEvent.AddListener(listener);
            instance.eventDictionaryParamInt.Add(eventName, thisEvent);
        }
    }
    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (messageManager == null) return;
        UnityEventInt thisEvent = null;
        // Try to get unity event by event name and remove its listener
        if (instance.eventDictionaryParamInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    public static void TriggerEvent(string eventName, int value)
    {
        Debug.Log("MessageManager tries to trigger " + eventName);
        UnityEventInt thisEvent = null;
        if (instance.eventDictionaryParamInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(value);
            Debug.Log("MessageManager successfully triggers " + eventName + " with value " + value.ToString());
        }
    }

}