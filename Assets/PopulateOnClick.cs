using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateOnClick : MonoBehaviour
{
    [Header("ONCLICKASSIGNERS")] 
    [SerializeField] private List<Button> onclickers = new();
    void Start()
    {
        foreach (Button b in onclickers)
        {
            b.onClick.AddListener(SettingsScript.instance.soundManager.ButtonClick);
        }
    }
}
