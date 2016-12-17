using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public float spawnInterval = 10f;
    public Transform[] spawnPoints;
    public Transform[] movePoints;

    void Awake()
    {
        MessageManager.StartListening("EnemyManagerBegin", StartSpawn);
        MessageManager.StartListening("PlayerDie", StopSpawn);
    }

    void OnDisable()
    {
        MessageManager.StopListening("EnemyManagerBegin", StartSpawn);
        MessageManager.StopListening("PlayerDie", StopSpawn);
    }
    void StartSpawn ()
    {
        Debug.Log("Start Spawn Enemy");
        InvokeRepeating("Spawn", spawnTime, spawnInterval);
    }
    void StopSpawn()
    {
        CancelInvoke("Spawn");
    }

    void Spawn ()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        GameObject tmp = Instantiate(enemies[enemyIndex], spawnPoints[enemyIndex].position, spawnPoints[enemyIndex].rotation) as GameObject;
        EnemyController tmpc = tmp.GetComponent<EnemyController>();
        tmpc.EnemyPlusInit(enemyIndex);
        tmpc.MovePoints = movePoints;
    }
}
