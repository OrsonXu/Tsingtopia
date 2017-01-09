using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMagic : MonoBehaviour {

    [HideInInspector]
    public int MaxMagic { get; set; }
    [HideInInspector]
    public int CurrentMagic { get; set; }
    [HideInInspector]
    public int RecoverRate { get; set; }
    public Slider AmmoSlider;

    private float _timeClock = 0;
    /// <summary>
    /// Override, register a message event
    /// </summary>
    private void OnEnable()
    {
        MessageManager.StartListening("PlayerChangeMagic", ChangeMagic);
    }
    /// <summary>
    /// Override, unregister a message event
    /// </summary>
    private void OnDisable()
    {
        MessageManager.StopListening("PlayerChangeMagic", ChangeMagic);
    }
    /// <summary>
    /// Change the magic value of the player
    /// </summary>
    /// <param name="value"></param>
    public void ChangeMagic(int value)
    {
        CurrentMagic += value;
        AmmoSlider.value = CurrentMagic;
        // Chump back to the max magic
        if (CurrentMagic > MaxMagic)
        {
            CurrentMagic = MaxMagic;
        }
        if (CurrentMagic < 0)
        {
            CurrentMagic = 0;
        }
    }
    /// <summary>
    /// Flag for whether player can useskill
    /// </summary>
    /// <param name="value">The needed magic value for some skill</param>
    /// <returns>return true if magic is greater than acquired magic value. Else return false</returns>
    public bool CanUseSkill(int value)
    {
        if (CurrentMagic >= value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        _timeClock += Time.deltaTime;
        // Recover MP automatically
        if (_timeClock > ((float)60 / (float)RecoverRate))
        {
            _timeClock = 0;
            ChangeMagic(1);
        }
        
    }
}
