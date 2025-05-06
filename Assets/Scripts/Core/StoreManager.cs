using NUnit.Framework.Internal;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button[] buttons;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(delegate { Select(button.name); });
        }
    }
    public void Buy()
    {
        foreach(var button in buttons)
        {
            var buttonSelect = button.GetComponent<CharacterSelect>();
            if (buttonSelect != null)
            {
                if (buttonSelect.selected && !buttonSelect.locked)
                {
                    Debug.Log("Character ALREADY Unlocked" + buttonSelect.name);
                }
                else if(buttonSelect.selected && buttonSelect.locked)
                {
                    if (gameManager.coins >= buttonSelect.cost)
                    {
                        gameManager.coins -= buttonSelect.cost;
                        buttonSelect.locked = false;
                        Debug.Log("NEW Character Unlocked" + buttonSelect.name);
                    }
                    else
                        Debug.Log("NEED MORE COINS");
                }
            }
        }
    }
    public void Select(String test)
    {
        foreach (var button in buttons)
        {
            if(button.name != test)
            {
                button.GetComponent<CharacterSelect>().selected = false;
            }
            else
            {
               button.GetComponent<CharacterSelect>().selected = true;
                Debug.Log("Character Selected:" + test);
            }
        }
    }
}
