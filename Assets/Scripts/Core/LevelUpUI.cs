using System.Collections;
using System.Threading;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public GameObject confetti;
    private void Start()
    {
        gameObject.SetActive(false);
        confetti.SetActive(false);
    }
    public void TriggerLevelUpMenus()
    {
        gameObject.SetActive(true);
        confetti.SetActive(true);
        StartCoroutine(PauseMouse());
        
        Debug.Log("LEVELUP");
    }
    public void CloseLevelUpMenu()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        confetti.SetActive(false);
        Time.timeScale = 1;
    }
    IEnumerator PauseMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
