using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingAssigner : MonoBehaviour
{
    private SettingsScript settings;
    [Header("Sliders")]
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Toggle damNumbers;

    [Header("ONCLICKASSIGNERS")] 
    [SerializeField] private List<Button> onclickers = new();
    
    private void Start()
    {
        settings = SettingsScript.instance;
        foreach (Button b in onclickers)
        {
            b.onClick.AddListener(settings.soundManager.ButtonClick);
        }

        MusicSlider.onValueChanged.AddListener(settings.UpdateMusicSoundSettings);
        SFXSlider.onValueChanged.AddListener(settings.UpdateSFXSoundSettings);
        damNumbers.onValueChanged.AddListener(settings.UpdateDamageNumbers);
        MusicSlider.value = settings.music.volume;
        SFXSlider.value = settings.soundManager.volume;
        damNumbers.isOn = settings.damNUmIsOn;
    }
}
