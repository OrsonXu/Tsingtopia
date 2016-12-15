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
        nav = GetComponent <NavMeshAgent> ();
        nav.speed = Speed;
    }

    void Update ()
    {
        nav.speed = Speed;
    }
    void OnDisable()
    {
        nav.enabled = false;
    }
    void OnEnable()
    {
        nav.enabled = true;
    }
    public void SetDestination(Vector3 pos)
    {
        nav.destination = pos;
    }

    public bool CloseEnough()
    {
        if (nav.remainingDistance < nav.stoppingDistance && !nav.pathPending)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
