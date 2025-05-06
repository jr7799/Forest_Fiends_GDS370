using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenu;
    private void Start()
    {
       // PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
    }
    public void Resume()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}
