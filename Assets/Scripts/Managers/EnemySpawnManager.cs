using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public float spawnInterval = 10f;
    public Transform[] spawnPoints;
    public Transform[] movePoints;

    void OnEnable()
    {
        MessageManager.StartListening("EnemyManagerBegin", StartSpawn);
        MessageManager.StartListening("PlayerFinish", StopSpawn);
        MessageManager.StartListening("PlayerDie", StopSpawn);
    }

    void OnDisable()
    {
        MessageManager.StopListening("EnemyManagerBegin", StartSpawn);
        MessageManager.StopListening("PlayerFinish", StopSpawn);
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
        EnemyManager tmpc = tmp.GetComponent<EnemyManager>();
        tmpc.EnemyPlusInit(enemyIndex);
        tmpc.MovePoints = movePoints;
    }
}
