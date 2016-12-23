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
		"二校门的墙上好像刻着什么东西...",
		"...",
		"2017年，时空虫洞无意被打开，3017年的智能机器族野心暴起，大举入侵清华，意图提前机器纪元的到来...人类陷入莫大恐慌...人机战役提前五百年被打响...",
		"2037年，经过无数人的辛勤研究，找到了克制来自千年以后的机器人的有效方法，时空虫洞被重新填补，危机暂时缓解。但是清华在这一战役中受到极大波及，整个校园几乎被毁...",
		"2057年，清华园重建完成，陆续开放各个院系，重新恢复了全球第一大学的生机与活力。",
		"2077年，时空虫洞被重新打开，与2017年的虫洞轨迹发生冲突，两个虫洞间出现干扰...",
		"...",
		"！！！这二校门到底是何时建造的？！如此新的历史！甚至预言了现在！难道是..未来的人建造的？",
		"...头又开始痛了...想不清...",
		"得把这个重大发现告诉董老师！"
	};
	private string[] task4 = {
		"战役提前五百年被打响...",
		"2037年，经过无数人的辛勤研究，找到了克制来自千年以后的机器人的有效方法，时空虫洞被重新填补，危机暂时缓解。但是清华在这一战役中受到极大波及，整个校园几乎被毁...",
		"2057年，清华园重建完成，陆续开放各个院系，重新恢复了全球第一大学的生机与活力。",
		"2077年，时空虫洞被重新打开，与2017年的虫洞轨迹发生冲突，两个虫洞间出现干扰...",
		"...",
		"！！！这二校门到底是何时建造的？！如此新的历史！甚至预言了现在！难道是..未来的人建造的？",
		"...头又开始痛了...想不清...",
		"得把这个重大发现告诉董老师！"
	};
	private string[] task5 = {
		"教室中有三个黑色的圆球在翻滚，却看不清里面的东西...",
		"那是...机器人！！！",
		"他们果然来入侵了！！",
		"先消灭各5个，看一看他们的能力，再回去告诉董老师！"

	};
	private string[] task6 = {
		"董老师！我消灭了一些机器人，看起来它们似乎不是很厉害。",
		"但是没有来得及查看线索，就跑出来了...",


		"这里人迹罕至，倒是有不少机器人神出鬼没..."
	};


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
		taskDialogues.Add(task1);
		taskDialogues.Add(task2);
		taskDialogues.Add(task3);
		taskDialogues.Add(task4);
		taskDialogues.Add(task5);
		taskDialogues.Add(task6);

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
