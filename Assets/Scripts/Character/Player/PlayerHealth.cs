using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{

    [HideInInspector]
    public int MaxHealth { get; set; }
    [HideInInspector] 
    public int CurrentHealth { get; set; }
    [HideInInspector]
    public int RecoverRate { get; set; }

    public Slider HealthSlider;
    public Image DamageImage;
    public AudioClip DeathClip;
    public AudioClip HurtClip;
    public float FlashSpeed = 5f;
    public Color FlashColour = new Color(1f, 0f, 0f, 0.1f);

    AudioSource _playerAudio;
    bool _isDead;
    bool _damaged;
    float _timeClock = 0;
    // Override, register a message event.
    private void OnEnable()
    {
        MessageManager.StartListening("PlayerChangeHealth", ChangeHealth);
    }
    // Override, unregister a message event.
    private void OnDisable()
    {
        MessageManager.StopListening("PlayerChangeHealth", ChangeHealth);
    }

    void Awake ()
    {
        _playerAudio = GetComponent<AudioSource>();
        _playerAudio.clip = HurtClip;
        _playerAudio.volume = 0.4f;
    }


    void Update ()
    {
        _timeClock += Time.deltaTime;
        
        // Recover HP automatically
        if (CurrentHealth > 0 && _timeClock > ((float)60 / (float)RecoverRate))
        {
            _timeClock = 0;
            ChangeHealth(1);
        }
        // If be hurted, flash image in the screeen
        if(_damaged)
        {
            DamageImage.color = FlashColour;
        }
        else
        {
            DamageImage.color = Color.Lerp (DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        _damaged = false;
    }

    /// <summary>
    /// Change the health of the player
    /// </summary>
    /// <param name="value">The value of the health change</param>
    public void ChangeHealth (int value)
    {
        if (!_isDead)
        {
            if (value < 0)
            {
                _damaged = true;
                _playerAudio.Play();
            }
            else
            {
                _damaged = false;
            }
            CurrentHealth += value;
            // Chump back to the max health
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            // Judge the death
            if (CurrentHealth <= 0)
            {
                Death();
                Debug.Log("health < 0");
            }
            HealthSlider.value = CurrentHealth;
        }
    }

    /// <summary>
    /// The function when the player dies, change the flag and play music
    /// </summary>
    void Death ()
    {
        _isDead = true;
        _playerAudio.clip = DeathClip;
        _playerAudio.volume = 0.8f;
        _playerAudio.Play ();
    }
    /// <summary>
    /// Public flag
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return _isDead;
    }

}
