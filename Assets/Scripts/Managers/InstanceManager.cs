using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstanceManager : MonoBehaviour {

    [SerializeField] PlayerController player;
    EnemyManager enemyManager;
    Command moveCommand;
    Command useSkillCommand;

    void Awake()
    {

        player.Initialization(100, 10, new Vector3(0f, 0f, 0f), new Vector3(0f, 90f, 0f));
        player.PlayerPlusInit();

        moveCommand = new MoveCommand();
        useSkillCommand = new UseSkillCommand();

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
        //useSkillCommand.execute(player);
        //moveCommand.execute(player);
        player.SwitchFSM();
    }

    public void RestartLevel()
    {
        //SceneManager.LoadScene (0);
        SceneManager.LoadScene("Scene01");
    }
}
