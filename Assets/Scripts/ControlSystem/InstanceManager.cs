using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : BaseManager {

    PlayerManager _playerController;
    EnemyManager _enemyManager;
    RecoverManager _recoverManager;

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



    void InitEnemy()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyManager.enabled = true;
    }
		

    void InitRecover()
    {
        _recoverManager = GetComponent<RecoverManager>();
        _recoverManager.enabled = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Opening");
    }
}
