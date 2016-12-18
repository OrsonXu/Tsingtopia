using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public enum TaskStatus{UNDISCOVERED, DISCOVERED, FINISHED};
public class TaskManager : BaseManager {
	//hold reference to events and events
	private Dictionary <string, UnityEvent> taskDictionary;
	private Dictionary<int, string> taskList;
	private LinkedList<int> activeTaskList;

	private static TaskManager taskManager;


	public override void Awake(){
		Debug.Log("TaskManager.Awake");
	}

	// Make taskManager can be got global easily
	public static TaskManager instance	{
		get	{
			if (!taskManager){
				taskManager = FindObjectOfType (typeof (TaskManager)) as TaskManager;
				if (!taskManager){
					Debug.LogError ("There needs to be one active TaskManager script on a GameObject in your scene.");
				}
				else{
					taskManager.Init (); 
				}
			}
			return taskManager;
		}
	}

	// Initialize taskManager
	void Init (){
		if (taskDictionary == null){
			taskDictionary = new Dictionary<string, UnityEvent>();
		}
		if (taskList == null){
			taskList = new Dictionary<int, string>();
		}

	}
		
	public static void RegisterTask(int taskID, string taskName){
		string thisTaskName = null;
		Debug.Log("TaskID" + taskID.ToString() + " added " + " name: " + taskName);
		// Try get value a eventName and add listener to it
		if (!instance.taskList.TryGetValue (taskID, out thisTaskName)){
			instance.taskList.Add (taskID, taskName);
		}
	}
	public static void UnregisterTask(int taskID, string taskName){

		if (taskManager == null) return;
		string thisTaskName = null;
		// Try to get unity event by event name and remove its listener
		if (instance.taskList.TryGetValue (taskID, out thisTaskName)){
			instance.taskList.Remove (taskID);
		}
	}
	public static string GetNameById(int taskID){
		string thisName = null;
		if (instance.taskList.TryGetValue (taskID, out thisName))
			;
		return thisName;
	}
	// Start listener
	public static void StartListening (string taskName, UnityAction listener){
		UnityEvent thisEvent = null;
		Debug.Log("MessageManager adds " + taskName);
		// Try get value a taskName and add listener to it
		if (instance.taskDictionary.TryGetValue (taskName, out thisEvent)){
			thisEvent.AddListener (listener);
		} 
		else{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.taskDictionary.Add (taskName, thisEvent);
		}
	}

	public static void StopListening (string taskName, UnityAction listener){
		if (taskManager == null) return;
		UnityEvent thisEvent = null;
		// Try to get unity event by event name and remove its listener
		if (instance.taskDictionary.TryGetValue (taskName, out thisEvent)){
			thisEvent.RemoveListener (listener);
		}
	}
	void printTaskList(){
		if (taskList != null) {
			foreach (KeyValuePair<int, string> kvp in taskList) {
				Debug.Log ("Key = {0}, Value = {1}" + kvp.Key + kvp.Value);
			}
		} else
			Debug.Log ("The taskList is null.");
	}
	public static void TriggerTask(string taskName){
		
//		Debug.Log("TaskManager tries to trigger " + taskName);
		UnityEvent thisEvent = null;
		if (instance.taskDictionary.TryGetValue (taskName, out thisEvent)) {
			thisEvent.Invoke ();
			Debug.Log ("TaskManager successfully triggers " + taskName);
		} else
			Debug.Log ("Failed to trigger " + taskName + ", cannot find this task in taskDictionary");

	}


}