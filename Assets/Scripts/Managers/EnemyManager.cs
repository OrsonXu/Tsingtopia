using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public float spawnInterval = 2f;
    public Transform[] spawnPoints;

    void Start ()
    {
        InvokeRepeating("Spawn", spawnTime, spawnInterval);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], spawnPoints[enemyIndex].position, spawnPoints[enemyIndex].rotation);
        
    }
}
