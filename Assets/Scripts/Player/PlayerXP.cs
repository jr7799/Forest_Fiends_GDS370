using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int playerLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int totalXP;
    [Header("UI")]
    public Image xpBarFill;  // Fill image of XP bar

    public UnityEvent LevelUpWhip;
    public UnityEvent LevelUpBible;
    public UnityEvent LevelUpGeneric;

    SoundManager soundManager;
    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    private void Update()
    {
        if (xpBarFill != null)
        {
            xpBarFill.fillAmount = (float)currentXP / xpToNextLevel;
        }
    }
    int menuTracker = 0;
    public void AddXP(int amount)
    {
        currentXP += amount;
        totalXP += amount;
        // Handle leveling up
        if (currentXP >= xpToNextLevel)
        {
            soundManager.LevelUpMusic();
            int temp = currentXP - xpToNextLevel;
            currentXP = 0;
            playerLevel++;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.35f); // Increase difficulty each level
            currentXP = temp;
            menuTracker++;
            if(menuTracker == 1)
            {
                if (LevelUpBible != null)
                    LevelUpBible.Invoke();
            }
            else if (menuTracker == 2)
            {
                if (LevelUpGeneric != null)
                    LevelUpGeneric.Invoke();
            }
            else if (menuTracker == 3)
            {
                if (LevelUpWhip != null)
                    LevelUpWhip.Invoke();
            }
            else if (menuTracker >= 4)
            {
                if (LevelUpGeneric != null)
                    LevelUpGeneric.Invoke();
            }
            Debug.Log("Level Up! New Level: " + playerLevel);
        }
    }
}
