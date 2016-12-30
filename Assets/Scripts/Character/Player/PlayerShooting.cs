using UnityEngine;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 100;
    public float timeBetweenBullets = 0.15f;
    public Image enemyImage;
    public Slider enemyHealthSlider;
    public float range = 100f;
    
    public int bulletMagicValue { get; set; }
    private GameObject enemyHealthObject;

    private float _timer;
    private Ray _shootRay;
    private RaycastHit _shootHit;
    private int _shootableMask;
    private ParticleSystem _gunParticles;
    private LineRenderer _gunLine;
    private AudioSource _gunAudio;
    private Light _gunLight;
    private float _effectsDisplayTime = 0.2f;

    void Awake ()
    {
        // Get components from public
        _shootableMask = LayerMask.GetMask ("Shootable");
        _gunParticles = GetComponent<ParticleSystem> ();
        _gunLine = GetComponent <LineRenderer> ();
        _gunAudio = GetComponent<AudioSource> ();
        _gunLight = GetComponent<Light> ();
        enemyImage.enabled = false;
        enemyHealthObject = enemyHealthSlider.gameObject;
        enemyHealthObject.SetActive(false);
        
    }


    void Update ()
    {
        _timer += Time.deltaTime;
        // If any input and the shoot frequency meets the requirement
		if(Input.GetButton ("Fire1") && _timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }
        // If time is longer enough
        if(_timer >= timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }
    /// <summary>
    /// The function when player shoots
    /// </summary>
    void Shoot ()
    {
        // Trigger the event of the magic change of the player
        MessageManager.TriggerEvent("PlayerChangeMagic", -bulletMagicValue);
        // Set the timestamp to zero
        _timer = 0f;
        // Play the music
        _gunAudio.Play ();

        _gunLight.enabled = true;
        // Trigger the particle system
        _gunParticles.Stop ();
        _gunParticles.Play ();

        _gunLine.enabled = true;
        _gunLine.SetPosition (0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        // If the bullet shoot something
        if(Physics.Raycast (_shootRay, out _shootHit, range, _shootableMask))
        {
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent <EnemyHealth> ();
            // If hit the enemy
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
                enemyImage.enabled = true;
                // Update the UI of enemy status
                enemyImage.sprite = enemyHealth.icon;
                enemyHealthObject.SetActive(true);
                enemyHealthSlider.value = ((float)enemyHealth.CurrentHealth / (float)enemyHealth.MaxHealth) * enemyHealthSlider.maxValue;
            }
            // else update the UI of enenmy state accordingly
            else
            {
                enemyImage.enabled = false;
                enemyHealthObject.SetActive(false);
            }
            _gunLine.SetPosition (1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition (1, _shootRay.origin + _shootRay.direction * range);
        }
    }
    /// <summary>
    /// Override, when the scripts is disabled.
    /// </summary>
    void OnDisable()
    {
        DisableEffects();
    }
}
