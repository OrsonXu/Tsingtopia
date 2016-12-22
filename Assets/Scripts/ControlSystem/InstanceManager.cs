using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : BaseManager {

    public override void Awake()
    {
		Debug.Log("InstanceManager.Awake");
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

        StartCoroutine("Finish");
    }

    private void FinishLevelFail()
    {
        // Show the fail diagolue
        
        StartCoroutine("Finish");
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WorldScene");
    }
}
