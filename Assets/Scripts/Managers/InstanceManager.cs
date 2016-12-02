using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour {

    [SerializeField] PlayerController playerController;
    EnemyManager enemyManager;
    RecoverManager recoverManager;

    void Awake()
    {
        playerController.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        playerController.PlayerPlusInit();
        playerController.InWorld = false;
        InitEnv();
        InitEnemy();
        InitRecover();

    }

    void InitEnv()
    {

    }

    void InitEnemy()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.enabled = true;
    }

    void Update()
    {
        playerController.FSMUpdate();
    }

    void InitRecover()
    {
        recoverManager = GetComponent<RecoverManager>();
        recoverManager.enabled = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Opening");
    }
}
