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
    public void TriggerLevelUpMenu()
    {
        gameObject.SetActive(true);
        confetti.SetActive(true);
        
        Time.timeScale = 0f;
        Debug.Log("LEVELUP");
    }
    public void CloseLevelUpMenu()
    {
        gameObject.SetActive(false);
        confetti.SetActive(false);
        Time.timeScale = 1;
    }
}
