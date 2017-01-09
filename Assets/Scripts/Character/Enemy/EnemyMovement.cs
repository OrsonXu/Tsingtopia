using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent _nav;
    public float Speed { get; set; }

    void Awake ()
    {
        _nav = GetComponent <NavMeshAgent> ();
        _nav.speed = Speed;
    }

    void Update ()
    {
        _nav.speed = Speed;
    }
    void OnDisable()
    {
        _nav.enabled = false;
    }
    void OnEnable()
    {
        _nav.enabled = true;
    }
    public void SetDestination(Vector3 pos)
    {
        _nav.destination = pos;
    }
    /// <summary>
    /// Flag for whether the enemy is close enough to the target
    /// </summary>
    /// <returns></returns>
    public bool CloseEnough()
    {
        if (_nav.remainingDistance < _nav.stoppingDistance && !_nav.pathPending)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
