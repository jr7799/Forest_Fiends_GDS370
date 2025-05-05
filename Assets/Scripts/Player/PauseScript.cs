using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject settingsMenu;
    public GameObject whipMenu;
    public GameObject bibleMenu;
    public GameObject rewardsMenu;
    private void Start()
    {
        PauseMenu.SetActive(false);
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (settingsMenu.activeSelf == false && whipMenu.activeSelf == false && bibleMenu.activeSelf == false && rewardsMenu.activeSelf == false)
        {
            if(PauseMenu.activeSelf == false)
                PauseMenu.SetActive(true);
            else
                PauseMenu.SetActive(false);
            Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
        }
    }
    public void Resume()
    {
        if (settingsMenu.activeSelf == false && whipMenu.activeSelf == false && bibleMenu.activeSelf == false && rewardsMenu.activeSelf == false)
        {
            if (PauseMenu.activeSelf == false)
                PauseMenu.SetActive(true);
            else
                PauseMenu.SetActive(false);
            Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
        }
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}
