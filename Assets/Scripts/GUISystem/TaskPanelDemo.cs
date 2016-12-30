using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskPanelDemo : MonoBehaviour {

	public Text taskText;
	public GameObject taskFac;
	private TaskFactory _taskFactory;
	private float _lastFrame;
	private const float _updateTime = 0.05f;
	private string _noTaskInfo = "现在没有任务";

	// Initialize this panel parameter 
	void Awake(){
        taskFac = GameObject.FindGameObjectWithTag("TaskFac");
		_taskFactory = taskFac.GetComponent<TaskFactory> ();
		_lastFrame = 0f;
	}
	// Update is called once per frame
	void Update () {
		if (_lastFrame < _updateTime) {
			_lastFrame += Time.deltaTime;
		}
		else{
			_lastFrame = 0f;
			if (TaskManager.activeTaskList.Count != 0) {
				string demoText = "";
				foreach(int i in TaskManager.activeTaskList){
					Task activeTask = _taskFactory.GetTaskByID(i);
					if (activeTask) {
						demoText += "**" + activeTask.getName () + "\n";
						demoText += activeTask.getDescription () + "\n";
					}
				}
				taskText.text = demoText;
			} else {
				taskText.text = _noTaskInfo;
			}
		}
	}
}
