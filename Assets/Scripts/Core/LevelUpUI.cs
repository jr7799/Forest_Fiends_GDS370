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
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
