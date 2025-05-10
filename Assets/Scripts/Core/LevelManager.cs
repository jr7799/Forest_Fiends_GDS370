using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public Button[] buttons;
    public GameManager gameManager;
    public Button selectedButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (selectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }
    }
    public void Select(String name)
    {
        foreach (var button in buttons)
        {
            var buttonSelect = button.GetComponent<LevelSelect>();
            if (button.name != name)
            {
                buttonSelect.selected = false;
            }
            else
            {
                buttonSelect.selected = true;
                gameManager.levelIndex = buttonSelect.levelIndex;
                gameManager.readyToPlay = true;
                selectedButton = button;

                Debug.Log("Level Selected Index:" + buttonSelect.levelIndex);
            }
        }
    }
}
