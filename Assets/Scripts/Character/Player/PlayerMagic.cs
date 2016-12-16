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

    float timeClock = 0;

    public void ChangeMagic(int value)
    {
        CurrentMagic += value;
        AmmoSlider.value = CurrentMagic;
        if (CurrentMagic > MaxMagic)
        {
            CurrentMagic = MaxMagic;
        }
    }

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
        timeClock += Time.deltaTime;
        if (timeClock > ((float)60 / (float)RecoverRate))
        {
            timeClock = 0;
            ChangeMagic(1);
        }
        
    }
}
