 using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public int MaxHealth { get; set; }
    [HideInInspector]
    public int CurrentHealth { get; set; }
    public float SinkSpeed = 2.5f;
    public int ScoreValue = 10;
    public AudioClip DeathClip;
    public AudioClip HurtClip;
    public Sprite icon;

    AudioSource _enemyAudio;
    ParticleSystem _hitParticles;
    CapsuleCollider _capsuleCollider;
    bool _isDead;
    bool _isSinking;

    void Awake ()
    {
        _enemyAudio = GetComponent<AudioSource>();
        _enemyAudio.clip = HurtClip;
        _enemyAudio.volume = 0.1f;

        _hitParticles = GetComponentInChildren <ParticleSystem> ();
        _capsuleCollider = GetComponent <CapsuleCollider> ();
    }


    void Update ()
    {
        if(_isSinking)
        {
            transform.Translate (-Vector3.up * SinkSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Change the health of the enemy
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="hitPoint"></param>
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(_isDead)
            return;

        _enemyAudio.Play ();
        CurrentHealth -= amount;
        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        if(CurrentHealth <= 0)
        {
            Death ();
        }
    }

    /// <summary>
    /// The function when the enemy dies. Change the flag and play the death music
    /// </summary>
    void Death ()
    {
        _isDead = true;

        _capsuleCollider.isTrigger = true;

        _enemyAudio.clip = DeathClip;
        _enemyAudio.volume = 0.6f;
        _enemyAudio.Play ();
    }

    /// <summary>
    /// enemy sink under the floor
    /// </summary>
    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        _isSinking = true;
        Destroy (gameObject, 2f);
    }
    /// <summary>
    /// Public flag for whether is enemy is dead
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return _isDead;
    }
}
