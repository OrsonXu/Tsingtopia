using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Task : MonoBehaviour {

	private int _taskID;
	private string _taskName;
	private TaskStatus _taskStatus;
	private string _taskDescription;
	private string[] _taskDialogue;
	private List<int> parentTasks;
	private List<int> childTasks;

	/// <summary>
	/// Get and set private pa
	/// </summary>
	/// <returns>The name.</returns>
	public string getName(){
		return _taskName;
	}
	public string getDescription(){
		return _taskDescription;
	}
	public int getId(){
		return _taskID;
	}
	public TaskStatus getStatus(){
		return _taskStatus;
	}
	public string[] getDialogue(){
		return _taskDialogue;
	}
	public string getStatusStr(){
		switch (_taskStatus) {
		case TaskStatus.DISCOVERED:
			return "DISCOVERED";
			break;
		case TaskStatus.UNDISCOVERED:
			return "UNDISCOVERED";
			break;
		case TaskStatus.FINISHED:
			return "FINISHED";
			break;
		default:
			return "none";
			break;
		}
	}
	public void setTaskProperty(int taskid, string taskname, TaskStatus status, string description){
		_taskID = taskid;
		_taskName = taskname;
		_taskStatus = status;
		_taskDescription = description;
        TaskManager.StartListening("Task" + _taskID.ToString() + "Trigger", TaskAction);
		TaskManager.StartListening("Task" + _taskID.ToString() + "TryDiscover", updateDiscovered);
        TaskManager.RegisterTask(_taskID, _taskName);
	}
	// set parent and child relations
	public void setRelations(int[] parent, int[] child){
		parentTasks = new List<int>();
		childTasks = new List<int>();
		for (int i = 0; i < parent.Length; i++) {
			parentTasks.Add( parent [i]);
		}
		for (int i = 0; i < child.Length; i++) {
			childTasks.Add( child [i]);
		}
	}

	public void setDialogues(string[] dia){
		this._taskDialogue = dia;
	}
    //void OnEnable(){
    //    TaskManager.StartListening ("Task" + _taskID.ToString(), TaskAction);
    //    TaskManager.RegisterTask (_taskID, _taskName);
    //}
	// Overwrite and unregister
	private void OnDisable(){
		TaskManager.StopListening ("Task" + _taskID.ToString() + "Trigger", TaskAction);
		TaskManager.StopListening ("Task" + _taskID.ToString() + "TryDiscover", updateDiscovered);
		TaskManager.UnregisterTask (_taskID, _taskName);
	}
	// Overwrite when this object is destroyed
	private void OnDestroy(){
		this.OnDisable ();
		Destroy (this.gameObject);
	}
	private void setStatus(TaskStatus ts){
		this._taskStatus = ts;

	}
	// When this task is discovered and update task relations
	private void updateDiscovered(){
		int unfinishedParent = 0;
		if (parentTasks != null) {
			GameObject[] TaskObjs = GameObject.FindGameObjectsWithTag("TaskObj"); 
			foreach (GameObject go in TaskObjs) {
				
				Task goTask = go.GetComponent<Task> ();
				int idx = parentTasks.BinarySearch (goTask.getId ());
				if (idx >= 0) {
					if (goTask.getStatus () != TaskStatus.FINISHED)
						unfinishedParent += 1;
				}

			}
//			for (int i = 0; i < parentTasks.Length; i++)
//				
//				if (parentTasks [i].getStatus () != TaskStatus.FINISHED)
//					unfinishedParent += 1;
		}
		if (unfinishedParent == 0) {
			this.setStatus (TaskStatus.DISCOVERED);
			TaskManager.activeListAdd (this._taskID);
		}

	}

	// Function when task is triggered
	private void TaskAction(){
		// Task change status according to 
		if (this._taskStatus == TaskStatus.DISCOVERED) {
			Debug.Log ("Task ID: " + _taskID.ToString () + "Task Operation");
			this.setStatus (TaskStatus.FINISHED);
			TaskManager.activeListRemove (this._taskID);
			if (childTasks.Count != 0) {
				foreach (int i in childTasks)
					TaskManager.TriggerTask ("Task" + i.ToString () + "TryDiscover");
			}
			GenericDialogueManager.Instance().DisplayMessage (_taskDialogue);
		}
	}

}
