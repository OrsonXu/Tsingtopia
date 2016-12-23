using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : BaseManager {

    public override void Awake()
    {
		Debug.Log("InstanceManager.Awake");

        // tmp hard code for convenience.
        GenericDialogueManager.Instance().DisplayMessage("消灭各种怪物5只");

        MessageManager.StartListening("PlayerDie", FinishLevelFail);
        MessageManager.StartListening("PlayerFinish", FinishLevelSuccess);
    }

    private void Start()
    {
        // Initiate player in the instance scene
        MessageManager.TriggerEvent("PlayerInit", false);

        Task task6 = GameObject.Find("TaskSystem/TaskFactory/task6").GetComponent<Task>();
        if (task6.getStatus() == TaskStatus.DISCOVERED)
        {
            string[] task = {
		    "教室中有三个黑色的圆球在翻滚，却看不清里面的东西...",
		    "那是...机器人！！！",
		    "他们果然来入侵了！！",
		    "先消灭各5个，看一看他们的能力，再回去告诉董老师！"
	        };
            GenericDialogueManager.Instance().DisplayMessage(task);
            // If the task6 is discoverd, then begin spwan enemy and recover
            
            MessageManager.TriggerEvent("EnemyManagerBegin");
            MessageManager.TriggerEvent("RecoverManagerBegin");
        }


    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Opening");
    }

    private void FinishLevelSuccess()
    {
        TaskManager.TriggerTask("Task6" + "Trigger");
        // Show the success dialogue
        GenericDialogueManager.Instance().DisplayMessage("已经消灭三种怪物各5只，先回去吧！");
        StartCoroutine("Finish");
    }

    private void FinishLevelFail()
    {
        // Show the fail diagolue
        GenericDialogueManager.Instance().DisplayMessage("机器人有点令人防不胜防，休息一会之后再来！");
        StartCoroutine("Finish");
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WorldScene");
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("z");
    }
}
