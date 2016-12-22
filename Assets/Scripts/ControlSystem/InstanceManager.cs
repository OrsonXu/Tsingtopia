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
        MessageManager.TriggerEvent("PlayerInit", false);
        MessageManager.TriggerEvent("EnemyManagerBegin");
        MessageManager.TriggerEvent("RecoverManagerBegin");
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
}
