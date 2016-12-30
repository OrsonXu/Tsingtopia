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
        //"<b>???</b>",
		"董老师:\n小清！你怎么在这儿",
		"小清:\n董老师，发生了什么...我这是...狗血的...穿越了...？？？",
		"董老师:\n...在软工课上有个叫Tsingtopia的小组在展示的时候，程序出现了bug...", 
        "董老师:\n直接导致了整个时空秩序的崩坏。我们都被吸入了一个巨大无比的虫洞中..", 
        "董老师:\n现在这儿..应该是60年后的清华园..", 
        "董老师:\n但是现在清华园人迹罕至，一直没有看到什么人...诡异之极啊..",
        "董老师:\n我也正在寻找能够回去的办法...",
		"小清:\n这...(说到底还是穿越了...)",
		"董老师:\n你可以去二校门查看一下情况吗？我怀疑那栋古老的建筑上可能会有一定的线索",
        "小清:\n...好"

	};

	private string[] task3 = {
		"二校门的墙上好像刻着什么东西...",
		"...",
		"2017年，时空虫洞无意被打开，3017年的智能机器族野心暴起，大举入侵清华，意图提前机器纪元的到来...人类陷入莫大恐慌...人机战役提前五百年被打响...",
		"2037年，经过无数人的辛勤研究，找到了克制来自千年以后的机器人的有效方法，时空虫洞被重新填补，危机暂时缓解。但是清华在这一战役中受到极大波及，整个校园几乎被毁...",
		"2057年，清华园重建完成，陆续开放各个院系，重新恢复了全球第一大学的生机与活力。",
		"2077年，时空虫洞被重新打开，与2017年的虫洞轨迹发生冲突，两个虫洞间出现干扰...",
		"...",
		"小清:\n！！！",
        "小清:\n这二校门到底是何时建造的？！如此新的历史！甚至预言了现在！",
        "小清:\n难道是..未来的人建造的？",
		"小清:\n...头又开始痛了...想不清...",
		"小清:\n得把这个重大发现告诉董老师！"
	};
	private string[] task4 = {
		"小清:\n董老师！二校门上面刻着十分诡异的东西！",
        "小清:\n@#￥%……&%￥#@！#￥%",

        "董老师:\n......",
        "董老师:\n是这样了...",
        "董老师:\n两个虫洞干涉重叠，怕是2017年机器入侵的情况，又要在2077年重演！而且...恐怕是同一批机器人！",
        "董老师:\n因为他们来自同一个时空，但是却因为虫洞交叉，而被传送到了两个不同的时空！",
        "董老师:\n而反过来...如果我们能够在2077年阻止他们，那么2017年的敌人也将消失！人类就会免受20年的机器侵扰！清华园也得以保存！",
        "董老师:\n这是一次60年后的莫大危机，也是莫大的机会...",
        "董老师:\n既然这样...",
        "董老师:\n小清！能否麻烦你去查看一下三教的情况，里面可能有六十年前我无意中放的一台设备...",
        "董老师:\n它也许能够帮助我们对抗机器人！",

        "小清:\n好的！"
	};
    private string[] task5 = { 
        "三教好像有股引力在吸引着我..."
    };
	private string[] task6 = {
        //"教室中有三个黑色的圆球在翻滚，却看不清里面的东西...",
        //"那是...机器人！！！",
        //"他们果然来入侵了！！",
        //"先消灭各5个，看一看他们的能力，再回去告诉董老师！"
        "这些机器人能力似乎一般，先回去告诉董老师吧！"
	};
	private string[] task7 = {
		"董老师！我消灭了一些机器人，看起来它们似乎不是很厉害。",
		"但是没有来得及查看线索，就跑出来了...",
	};


    private List<string[]> _taskDialogues;

    void Awake()
    {
        _taskID = new List<int>();
        _taskName = new List<string>();
        _taskDescription = new List<string>();
        _taskDialogues = new List<string[]>();
        _taskNumberCounter = 0;
    }

	void Start(){
		InitParam ();
		ConstructTaskGraph();
		InitTasks();
	}
    /// <summary>
    /// Initiate all of the task ID/name/description/dialogue
    /// </summary>
	private void InitParam(){
		string name;
		string description;
		_taskDialogues.Add(task1);
		_taskDialogues.Add(task1);
		_taskDialogues.Add(task2);
		_taskDialogues.Add(task3);
		_taskDialogues.Add(task4);
		_taskDialogues.Add(task5);
		_taskDialogues.Add(task6);
        _taskDialogues.Add(task7);


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
        //TaskManager.activeListAdd (1);
        //TaskManager.TriggerTask ("Task1Trigger");
    }
    /// <summary>
    /// Construct the task graph
    /// </summary>
    private void ConstructTaskGraph()
    {
		
        for (int i = 1; i <= _taskID.Count; i++)
        {
            int[] p = {i - 1};
            int[] c = {i + 1};
            // Special instantiate for the first one
            if (i == 1)
            {
                Debug.Log(_taskName[i]);
                InstantiateTask(_taskID[i], _taskName[i], TaskStatus.DISCOVERED,
                    _taskDescription[i], _taskDialogues[i], p, c);
            }
            else
            {
                Debug.Log(_taskName[i]);
                InstantiateTask(_taskID[i], _taskName[i], TaskStatus.UNDISCOVERED,
					_taskDescription[i], _taskDialogues[i], p, c);
            }
        }
        
    }
    /// <summary>
    /// Initiate by id, taskname, status, description
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="taskname"></param>
    /// <param name="status"></param>
    /// <param name="description"></param>
    public void InstantiateTask(int taskid, string taskname, TaskStatus status, string description)
    {
        GameObject tmpTaskObj = Instantiate(TaskObj) as GameObject;
        tmpTaskObj.transform.parent = this.transform;
        tmpTaskObj.name = "task" + taskid.ToString();
        Task task = tmpTaskObj.GetComponent<Task>();
        task.setTaskProperty(taskid, taskname, status, description);
    }
    /// <summary>
    /// Initiate by id, taskname, status, description, parent list, children list
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="taskname"></param>
    /// <param name="status"></param>
    /// <param name="description"></param>
    /// <param name="parent"></param>
    /// <param name="child"></param>
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
    /// <summary>
    /// Initiate by id, taskname, status, description, dialogue list, parent list, children list
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="taskname"></param>
    /// <param name="status"></param>
    /// <param name="description"></param>
    /// <param name="dialogues"></param>
    /// <param name="parent"></param>
    /// <param name="child"></param>
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
    /// <summary>
    /// Get the task by ID
    /// </summary>
    /// <param name="taskid"></param>
    /// <returns></returns>
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
