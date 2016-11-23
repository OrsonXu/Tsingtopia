using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour {

    [SerializeField] PlayerController player;
    EnemyManager enemyManager;

    void Awake()
    {
        player.Init(new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        player.PlayerPlusInit();

        InitEnv();
        InitEnemy();
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

    public void RestartLevel()
    {
        SceneManager.LoadScene("Opening");
    }
}
