using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TaskFactory : MonoBehaviour {

    public GameObject TaskObj;

    private int _taskNumberCounter;
    private List<int> _taskID;
    private List<string> _taskName;
    private List<string> _taskDescription;
	private string[] names =
    {"开始游戏","拜访董渊老师",
      "探索二校门", "第一个结论",
      "探索三教", "第一场战斗",
      "初战告捷"};
	private string[] descriptions = 
    { "","校园里发生了异变，去问问董老师，老师应该知道些什么…",
      "二校门处也许有线索…", "赶紧把这一发现告诉董老师。",
      "进入三教，查看设备，那里应该是异变刚开始的地方。", "消灭三种敌人各五个",
      "返回董老师处报告情况。"};
    void Awake()
    {
        //Test();
        _taskID = new List<int>();
        _taskName = new List<string>();
        _taskDescription = new List<string>();

        _taskNumberCounter = 0;
       

    }
	void Start(){
		InitParam ();
		ConstructTaskGraph();
		InitTasks();

	}

	private void InitParam(){
		string name;
		string description;
		// Zero
		name = "";
		description = "";
		_taskID.Add(0);
		_taskName.Add(name);
		_taskDescription.Add(description);

		for (int i = 0; i < names.Length; i++) {
			Debug.Log ("************");
			_taskID.Add(++_taskNumberCounter);
			_taskName.Add(names[i]);
			_taskDescription.Add(descriptions[i]);
		}
	}
    private void InitTasks()
    {
        
		TaskManager.activeListAdd (1);
		TaskManager.TriggerTask ("Task1Trigger");


    }

    private void ConstructTaskGraph()
    {
        for (int i = 1; i < _taskID.Count; i++)
        {
            int[] p = {i - 1};
            int[] c = {i + 1};
            if (i == 1)
            {
                Debug.Log(_taskName[i]);
                InstantiateTask(_taskID[i], _taskName[i], TaskStatus.DISCOVERED,
                    _taskDescription[i], p, c);
            }
            else
            {
                Debug.Log(_taskName[i]);
                InstantiateTask(_taskID[i], _taskName[i], TaskStatus.UNDISCOVERED,
                    _taskDescription[i], p, c);
            }
        }
        
    }

    public void InstantiateTask(int taskid, string taskname, TaskStatus status, string description)
    {
        GameObject tmpTaskObj = Instantiate(TaskObj) as GameObject;
        tmpTaskObj.transform.parent = this.transform;
        tmpTaskObj.name = "task" + taskid.ToString();
        Task task = tmpTaskObj.GetComponent<Task>();
        task.setTaskProperty(taskid, taskname, status, description);
    }

    public void InstantiateTask(int taskid, string taskname, TaskStatus status, string description,
                                int[] parent, int[] child)
    {
        GameObject tmpTaskObj = Instantiate(TaskObj) as GameObject;
        tmpTaskObj.transform.parent = this.transform;
        tmpTaskObj.name = "task" + taskid.ToString();
        Task task = tmpTaskObj.GetComponent<Task>();
        task.setTaskProperty(taskid, taskname, status, description);
        task.setRelations(parent, child);
    }
    public Task GetTaskByID(int taskid)
    {
        GameObject tmpTaskObj = GameObject.Find("TaskFactory/task" + taskid.ToString());
        if (tmpTaskObj)
        {
            return tmpTaskObj.GetComponent<Task>();
        }
        else
        {
            return null;
        }
    }

}
