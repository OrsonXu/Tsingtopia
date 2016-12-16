using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    public float Speed { get; set; }
    

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent <NavMeshAgent> ();
        nav.speed = Speed;
    }

    void Update ()
    {
        nav.speed = Speed;
        //if (enemyHealth.CurrentHealth > 0 && playerHealth.CurrentHealth > 0)
        //{
        if (player != null)
        {
            nav.SetDestination(player.position);
            nav.enabled = true;
        }
        
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
    void OnDisable()
    {
        nav.enabled = false;
    }
}
