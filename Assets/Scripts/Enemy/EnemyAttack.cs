using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool InRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player" && !other.isTrigger)
        {
            InRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            InRange = false;
        }
    }
    

    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && InRange && enemyHealth.CurrentHealth > 0)
        {
            Attack ();
        }

    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.CurrentHealth > 0)
        {
            playerHealth.ChangeHealth (-attackDamage);
        }
    }
}
