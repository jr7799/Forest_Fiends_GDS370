using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseMenu;
    private void Start()
    {
       PauseMenu.SetActive(false);
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
            if (PauseMenu.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
            

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
