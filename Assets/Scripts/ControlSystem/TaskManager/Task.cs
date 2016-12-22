using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Task : MonoBehaviour {

	private int _taskID;
	private string _taskName;
	private TaskStatus _taskStatus;
	private string _taskDescription;
	private List<int> parentTasks;
	private List<int> childTasks;
	void Awake(){
		_taskID = 1;
		_taskName = "Wow";
		_taskStatus = 0;
		_taskDescription = "Seikai heiwa";
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
    //void OnEnable(){
    //    TaskManager.StartListening ("Task" + _taskID.ToString(), TaskAction);
    //    TaskManager.RegisterTask (_taskID, _taskName);
    //}

	private void OnDisable(){
		TaskManager.StopListening ("Task" + _taskID.ToString() + "Trigger", TaskAction);
		TaskManager.StopListening ("Task" + _taskID.ToString() + "TryDiscover", updateDiscovered);
		TaskManager.UnregisterTask (_taskID, _taskName);
	}

	private void OnDestroy(){
		this.OnDisable ();
		Destroy (this.gameObject);
	}
	private void setStatus(TaskStatus ts){
		this._taskStatus = ts;

	}
	private int getId(){
		return this._taskID;
	}
	private TaskStatus getStatus(){
		return this._taskStatus;
	}

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
	private void TaskAction(){
		// Task change status according to 
		Debug.Log("Task Operation");
		this.setStatus (TaskStatus.FINISHED);
		TaskManager.activeListAdd (this._taskID);
		if (childTasks.Count != 0) {
			foreach(int i in childTasks)
				TaskManager.TriggerTask ("Task"+i.ToString()+"TryDiscover");
		}

	}

}
