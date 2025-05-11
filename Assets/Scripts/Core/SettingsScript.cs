using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsScript : MonoBehaviour, IDataPersistance
{

    public SoundManager soundManager;
    public BGMusic music;
    [Header("Sliders")]
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Toggle damNumbers;
    public bool damNUmIsOn;


    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        music = GameObject.Find("BGMusicManager").GetComponent<BGMusic>();
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateSoundSettings();
        UpdateDamageNumbers();
    }
    public void UpdateSoundSettings()
    {
        music.volume = MusicSlider.value;
        soundManager.volume = SFXSlider.value;
    }
    public void UpdateDamageNumbers()
    {
        damNUmIsOn = damNumbers.isOn;
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
