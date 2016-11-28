using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour {

    [SerializeField] PlayerController player;
    EnemyManager enemyManager;
    RecoverManager recoverManager;

    void Awake()
    {
        player.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        player.PlayerPlusInit();

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
        player.FSMUpdate();
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
