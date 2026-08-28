
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button[] buttons;
    public GameManager gameManager;
    public Button selectedButton;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(delegate { Select(button.name); });
        }
    }
    private void Update()
    {
        //gets the current selected gameobject which can include buttons
        if (selectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);

            if (selectedButton.GetComponent<CharacterSelect>().locked == false)
            {
                gameManager.characterSelected = true;
            }
            else
            {
                gameManager.characterSelected = false;
            }
        }
    }
    public void Buy()
    {
        if(selectedButton != null)
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        foreach (var button in buttons)
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
                        gameManager.characterSelected = true;
                        Debug.Log("NEW Character Unlocked" + buttonSelect.name);
                    }
                    else
                        Debug.Log("NEED MORE COINS");
                }
            }
        }
    }
    public void Select(String name)
    {
        foreach (var button in buttons)
        {
            var buttonSelect = button.GetComponent<CharacterSelect>();
            if (button.name != name)
            {
                buttonSelect.selected  = false;
            }
            else
            {
                buttonSelect.selected = true;
                gameManager.playerSprite = buttonSelect.playerSprite;
                gameManager.playerAnimation = buttonSelect.playerAnimation;
                gameManager.playerWeapon = buttonSelect.playerWeapon;
                selectedButton = button;

                Debug.Log("Character Selected:" + name);
            }
        }
    }
}
