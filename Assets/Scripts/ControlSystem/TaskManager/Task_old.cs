using UnityEngine;
using System.Collections;


public class Task_old : MonoBehaviour {

	private int _taskID;
	private string _taskName;
	private TaskStatus _taskStatus;
	private string _taskDescription;
	private int[] parentTasks;
	private int[] childTasks;
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
	}
	public void setRelations(int[] parent, int[] child){
		parentTasks = new int[parent.Length];
		childTasks = new int[child.Length];
		for (int i = 0; i < parent.Length; i++) {
			parentTasks [i] = parent [i];
		}
		for (int i = 0; i < child.Length; i++) {
			childTasks [i] = child [i];
		}
	}
	void OnEnable(){
		TaskManager.StartListening ("Task" + _taskID.ToString(), TaskAction);
		TaskManager.RegisterTask (_taskID, _taskName);
	}
	void OnDisable(){
		TaskManager.StopListening ("Task" + _taskID.ToString(), TaskAction);
		TaskManager.UnregisterTask (_taskID, _taskName);
	}

	void OnDestroy(){
		this.OnDisable ();
		Destroy (this.gameObject);
	}
	private void TaskAction(){
		// Task change status according to 
		Debug.Log("Task Operation");

	}

}
