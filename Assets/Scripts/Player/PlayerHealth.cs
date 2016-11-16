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

    public Slider HealthSlider;
    public Image DamageImage;
    public AudioClip DeathClip;
    public AudioClip HurtClip;
    public float FlashSpeed = 5f;
    public Color FlashColour = new Color(1f, 0f, 0f, 0.1f);

    AudioSource playerAudio;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = HurtClip;
        playerAudio.volume = 0.1f;
    }


    void Update ()
    {
        if(damaged)
        {
            DamageImage.color = FlashColour;
        }
        else
        {
            DamageImage.color = Color.Lerp (DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        CurrentHealth -= amount;

        HealthSlider.value = CurrentHealth;

        playerAudio.Play ();

        if (CurrentHealth <= 0 && !isDead)
        {
            Death ();
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
