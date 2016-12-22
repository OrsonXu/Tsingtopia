using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : BaseManager {

    public override void Awake()
    {
		Debug.Log("InstanceManager.Awake");
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
}
