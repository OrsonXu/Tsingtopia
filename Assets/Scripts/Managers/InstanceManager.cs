using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour {

    PlayerController _playerController;
    EnemyManager _enemyManager;
    RecoverManager _recoverManager;

    void Awake()
    {
        //_playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //_playerController.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        //_playerController.PlayerPlusInit();
        //_playerController.InWorld = false;
        //InitEnv();
        //InitEnemy();
        //InitRecover();

    }

    private void Start()
    {
        MessageManager.TriggerEvent("PlayerInit");
        MessageManager.TriggerEvent("EnemyManagerBegin");
        MessageManager.TriggerEvent("RecoverManagerBegin");
    }



    private void OnDisable()
    {
        
    }

    void InitEnv()
    {

    }

    void InitEnemy()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyManager.enabled = true;
    }

    //void Update()
    //{
    //    _playerController.FSMUpdate();
    //}

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
