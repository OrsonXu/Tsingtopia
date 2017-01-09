using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public enum TaskStatus{UNDISCOVERED, DISCOVERED, FINISHED};
public class TaskManager : BaseManager {
	//hold reference to events and events
	public static List<int> activeTaskList;
	private Dictionary <string, UnityEvent> taskDictionary;
	private Dictionary<int, string> taskList;


	private static TaskManager taskManager;
	// Singleton model awake, ensure there is only one instancelized taskManager
	public override void Awake(){
		Init ();
        if (taskManager != null && taskManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            taskManager = this;
        }
        DontDestroyOnLoad(this.gameObject);
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
	// Register Task
	public static void RegisterTask(int taskID, string taskName){
		string thisTaskName = null;
		Debug.Log("TaskID" + taskID.ToString() + " added " + " name: " + taskName);
		// Try get value a eventName and add listener to it
		if (!instance.taskList.TryGetValue (taskID, out thisTaskName)){
			instance.taskList.Add (taskID, taskName);
		}
	}
	// Unregister Task
	public static void UnregisterTask(int taskID, string taskName){

		if (taskManager == null) return;
		string thisTaskName = null;
		// Try to get unity event by event name and remove its listener
		if (instance.taskList.TryGetValue (taskID, out thisTaskName)){
			instance.taskList.Remove (taskID);
		}
	}
	// Return task string when input is task ID
	public static string GetNameById(int taskID){
		string thisName = null;
		if (instance.taskList.TryGetValue (taskID, out thisName))
			;
		return thisName;
	}
	// Add task to actived list
	public static void activeListAdd(int taskid){
		activeTaskList.Add (taskid);
	}
	// Remove task from actived list
	public static void activeListRemove(int taskid){
		activeTaskList.Remove (taskid);
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

	// Stop a task listener
	public static void StopListening (string taskName, UnityAction listener){
		if (taskManager == null) return;
		UnityEvent thisEvent = null;
		// Try to get unity event by event name and remove its listener
		if (instance.taskDictionary.TryGetValue (taskName, out thisEvent)){
			thisEvent.RemoveListener (listener);
		}
	}

	// Trigger a task
	public static void TriggerTask(string taskName){

		//		Debug.Log("TaskManager tries to trigger " + taskName);
		UnityEvent thisEvent = null;
		if (instance.taskDictionary.TryGetValue (taskName, out thisEvent)) {
			thisEvent.Invoke ();
			Debug.Log ("TaskManager successfully triggers " + taskName);
		} else
			Debug.Log ("Failed to trigger " + taskName + ", cannot find this task in taskDictionary");

	}

	// get a task obj by its id
	public Task GetTaskById(int taskid){
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("TaskObj");
		Task rtnTask = null;
		foreach(GameObject go in gos){
			Task goTask = go.GetComponent<Task> ();
			if (goTask.getId () == taskid)
				rtnTask = goTask;
		}
		return rtnTask;

	}
	// Initialize taskManager
	void Init (){
		if (taskDictionary == null){
			taskDictionary = new Dictionary<string, UnityEvent>();
		}
		if (taskList == null){
			taskList = new Dictionary<int, string>();
		}
		if (activeTaskList == null) {
			activeTaskList = new List<int> ();
		}
			
	}

	// print task manager function, print task info in Unity debug console
	void print(){
		if (taskList != null) {
//			foreach (KeyValuePair<int, string> kvp in taskList) {
//				Debug.Log ("Key = "+ kvp.Key.ToString() +" " + "Value = " + kvp.Value.ToString());
//			}

			GameObject[] gos = GameObject.FindGameObjectsWithTag ("Task");
			string demoList = "";
			foreach (GameObject go in gos) {
				Task goTask = go.GetComponent<Task> ();
				demoList += " -- taskid " + goTask.getId() + " taskstatus" + goTask.getStatusStr() +
					" taskname "+goTask.getName() + "  " ;
				if (goTask.getDialogue () != null) {
                    demoList += "taskdescription" + goTask.getDialogue()[0] + "\n";
				}

			}
			Debug.Log (demoList);
			Debug.Log("Now activated tasks are :");
			if (activeTaskList.Count != 0) {
				string demoText = "";
				foreach (int taskid in activeTaskList) {
					demoText += " -- task with id" + taskid.ToString () ;
				}
				Debug.Log (demoText);
			}
			else 
				Debug.Log ("Active task is null");
		} else
			Debug.Log ("The taskList is null.");
	}

	// Overwrite, when M pressed, print the debug info
	void Update(){
		if (Input.GetKey (KeyCode.M)) {
			print ();
		}
	}

}