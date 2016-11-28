using UnityEngine;
using UnityEngine.UI;


public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 100;
    public float timeBetweenBullets = 0.15f;
    public Image enemyImage;
    public Slider enemyHealthSlider;
    public float range = 100f;
    
    
    //[HideInInspector]
    public int bulletMagicValue { get; set; }
    private GameObject enemyHealthObject;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    PlayerMagic playerMagic;


    void Awake ()
    {
        // Get components from public
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        playerMagic = GetComponentInParent<PlayerMagic>();
        enemyImage.enabled = false;
        enemyHealthObject = enemyHealthSlider.gameObject;
        enemyHealthObject.SetActive(false);
        
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot ()
    {
        playerMagic.ChangeMagic(-bulletMagicValue);

        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                enemyImage.enabled = true;
                enemyImage.sprite = enemyHealth.icon;
                enemyHealthObject.SetActive(true);
                enemyHealthSlider.value = ((float)enemyHealth.CurrentHealth / (float)enemyHealth.MaxHealth) * enemyHealthSlider.maxValue;
            }
            else
            {
                enemyImage.enabled = false;
                enemyHealthObject.SetActive(false);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void OnDisable()
    {
        DisableEffects();
    }
}
