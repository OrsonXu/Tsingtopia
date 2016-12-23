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
	private string[] task1 = {
		"唔，有点头痛...这节软工课睡的挺爽的...",
		"是不是有展示什么的...",
		"嗯？这是哪儿...？几点了...？",
		"什么？？？清华怎么变成这样子了...？？？",
		"现在是...20...2077年！！！",
		"怎么回事！！！",
		"诶！那边好像是董老师！！"
	};
	private string[] task2 = {
		"<b>???</b>",
		"小清！你怎么在这儿",
		"<b>小清:</b>",
		"董老师，发生了什么...我这是...狗血的...穿越了...？？？",
		"<b>董老师:</b>",
		"...在几小时前的软工课上，有个叫Tsingtopia的小组在展示的时候，程序出现了bug，直接导致了整个时空秩序的崩坏。我们都被吸入了一个巨大无比的虫洞中，现在这儿...应该是60年后的清华园... 但是现在清华园人迹罕至，我到现在都没有看到什么人...诡异之极啊..		我正在寻找能够回去的办法...",
		"<b>小清OS</b>",
		"这...（说到底还是穿越了...",
		"<b>董老师:</b>",
		"你可以去二校门查看一下情况吗？我怀疑那栋古老的建筑上可能会有一定的线索"

	};

	private string[] task3 = {

	};
	private string[] task4 = null;
	private string[] task5 = null;
	private string[] task6 = null;


	private List<string[]> taskDialogues = new List<string[]>();

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
		taskDialogues.Add(task1);
		taskDialogues.Add(task2);
		taskDialogues.Add(task1);
		taskDialogues.Add(task1);
		taskDialogues.Add(task1);
		taskDialogues.Add(task1);

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
                    _taskDescription[i], taskDialogues[i], p, c);
            }
            else
            {
                Debug.Log(_taskName[i]);
                InstantiateTask(_taskID[i], _taskName[i], TaskStatus.UNDISCOVERED,
					_taskDescription[i], taskDialogues[i], p, c);
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
	public void InstantiateTask(int taskid, string taskname, TaskStatus status, string description,string[] dialogues,
		int[] parent, int[] child)
	{
		GameObject tmpTaskObj = Instantiate(TaskObj) as GameObject;
		tmpTaskObj.transform.parent = this.transform;
		tmpTaskObj.name = "task" + taskid.ToString();
		Task task = tmpTaskObj.GetComponent<Task>();
		task.setDialogues (dialogues);
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
