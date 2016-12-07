using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public float spawnInterval = 10f;
    public Transform[] spawnPoints;

    void Start ()
    {
        InvokeRepeating("Spawn", spawnTime, spawnInterval);
    }


    void Spawn ()
    {
        if (playerController.IsDead())
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemies.Length);
        GameObject tmp = Instantiate(enemies[enemyIndex], spawnPoints[enemyIndex].position, spawnPoints[enemyIndex].rotation) as GameObject;
        EnemyController tmpc = tmp.GetComponent<EnemyController>();
        tmpc.EnemyPlusInit(enemyIndex);
        
    }
}
