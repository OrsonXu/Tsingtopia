using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private EnemyHealth _enemyHealth;
    private bool _InRange;
    private float _timer;

    void Awake ()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void OnTriggerEnter (Collider other)
    {
        // If the player is in the alert range
        if(other.tag == "Player" && !other.isTrigger)
        {
            _InRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        // If the player leave out of the alert range
        if (other.tag == "Player" && !other.isTrigger)
        {
            _InRange = false;
        }
    }
    

    void Update ()
    {
        _timer += Time.deltaTime;
        // If the time is longer than the interval and the player is in the range and alive
        if(_timer >= timeBetweenAttacks && _InRange && _enemyHealth.CurrentHealth > 0)
        {
            // Trigger the event of changing player health
            MessageManager.TriggerEvent("PlayerChangeHealth", -attackDamage);
            _timer = 0;
        }

    }

}
