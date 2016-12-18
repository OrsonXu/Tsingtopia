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

    private void OnEnable()
    {
        MessageManager.StartListening("PlayerChangeMagic", ChangeMagic);
    }
    private void OnDisable()
    {
        MessageManager.StopListening("PlayerChangeMagic", ChangeMagic);
    }

    public void ChangeMagic(int value)
    {
        CurrentMagic += value;
        AmmoSlider.value = CurrentMagic;
        if (CurrentMagic > MaxMagic)
        {
            CurrentMagic = MaxMagic;
        }
        if (CurrentMagic < 0)
        {
            CurrentMagic = 0;
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
        _timeClock += Time.deltaTime;
        if (_timeClock > ((float)60 / (float)RecoverRate))
        {
            _timeClock = 0;
            ChangeMagic(1);
        }
        
    }
}
