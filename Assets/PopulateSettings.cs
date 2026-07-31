using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Toggle damNumbers;
    [Header("ONCLICKASSIGNERS")] 
    [SerializeField] private List<Button> onclickers = new();
    void Start()
    {
        if(SettingsScript.instance) SettingsScript.instance.PopulateSliders(MusicSlider, SFXSlider, damNumbers);
        foreach (Button b in onclickers)
        {
            b.onClick.AddListener(SettingsScript.instance.soundManager.ButtonClick);
        }
    }
}
