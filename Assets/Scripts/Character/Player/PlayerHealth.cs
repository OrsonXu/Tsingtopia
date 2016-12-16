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

    AudioSource playerAudio;
    bool isDead;
    bool damaged;
    float timeClock = 0;

    void Awake ()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = HurtClip;
        playerAudio.volume = 0.4f;
    }


    void Update ()
    {
        timeClock += Time.deltaTime;
        if (CurrentHealth > 0 && timeClock > ((float)60 / (float)RecoverRate))
        {
            timeClock = 0;
            ChangeHealth(1);
        }

        if(damaged)
        {
            DamageImage.color = FlashColour;
        }
        else
        {
            DamageImage.color = Color.Lerp (DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (CurrentHealth <= 0 && !isDead)
        {
            Death();
            Debug.Log("health < 0");
        }
        
    }


    public void ChangeHealth (int value)
    {
        if (!isDead)
        {
            if (value < 0)
            {
                damaged = true;
                playerAudio.Play();
            }
            else
            {
                damaged = false;
            }
            CurrentHealth += value;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            HealthSlider.value = CurrentHealth;
        }
    }


    void Death ()
    {
        isDead = true;

        playerAudio.clip = DeathClip;
        playerAudio.volume = 0.8f;
        playerAudio.Play ();
    }

    public bool IsDead()
    {
        return isDead;
    }

}
