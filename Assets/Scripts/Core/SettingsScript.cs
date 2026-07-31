using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsScript : MonoBehaviour, IDataPersistance
{
    public static SettingsScript instance;
    public SoundManager soundManager;
    public BGMusic music;
    [Header("Sliders")]
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Toggle damNumbers;
    public bool damNUmIsOn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PopulateSliders(Slider m, Slider sfx, Toggle dam)
    {
        soundManager = SoundManager.instance;
        music = BGMusic.instance;
        if (MusicSlider == null) MusicSlider = m;
        if (SFXSlider == null) SFXSlider = sfx;
        if (damNumbers == null) damNumbers = dam;
        MusicSlider.onValueChanged.AddListener(UpdateMusicSoundSettings);
        SFXSlider.onValueChanged.AddListener(UpdateSFXSoundSettings);
        damNumbers.onValueChanged.AddListener(UpdateDamageNumbers);
        MusicSlider.value = music.volume;
        SFXSlider.value = soundManager.volume;
        damNumbers.isOn = damNUmIsOn;
    }
    void Start()
    {
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager = SoundManager.instance;
        music = BGMusic.instance;
       
        MusicSlider.onValueChanged.AddListener(UpdateMusicSoundSettings);
        SFXSlider.onValueChanged.AddListener(UpdateSFXSoundSettings);
        damNumbers.onValueChanged.AddListener(UpdateDamageNumbers);
        MusicSlider.value = music.volume;
        SFXSlider.value = soundManager.volume;
        damNumbers.isOn = damNUmIsOn;
    }
    
    public void UpdateMusicSoundSettings(float value)
    {
        music.volume = value;
    } public void UpdateSFXSoundSettings(float value)
    {
        soundManager.volume = value;
    }
    public void UpdateDamageNumbers(bool value)
    {
        damNUmIsOn = value;
    }
    public void LoadData(GameData data)
    {
        damNumbers.isOn = data.damageNumbersOn;
        MusicSlider.value = data.MusicVolume;
        SFXSlider.value = data.SFXMusic;
    }

    public void SaveData(GameData data)
    {
        data.damageNumbersOn = damNumbers.isOn;
        data.MusicVolume = MusicSlider.value;
        data.SFXMusic = SFXSlider.value;
    }
    
}
