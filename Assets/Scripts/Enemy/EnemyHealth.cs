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

    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.clip = HurtClip;
        enemyAudio.volume = 0.1f;

        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * SinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();
        CurrentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(CurrentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        enemyAudio.clip = DeathClip;
        enemyAudio.volume = 0.6f;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        Destroy (gameObject, 2f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
