using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Stats")]
    public int totalSpawned;
    public int totalKilled;
    public int minutes;
    public int seconds;
    [Header("Gem Management")]
    public int totalGems;
    public List<GameObject> gems = new List<GameObject>();
    public bool redGemSpawned = false;

    public TMP_Text killCounter;

    static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (killCounter != null)
        {
            if (totalKilled < 10)
            {
                killCounter.text = "0000" + totalKilled.ToString();
            }
            else if (totalKilled >= 10 && totalKilled < 100)
            {
                killCounter.text = "000" + totalKilled.ToString();
            }
            else if (totalKilled >= 100 && totalKilled < 1000)
            {
                killCounter.text = "00" + totalKilled.ToString();
            }
            else if (totalKilled >= 1000 && totalKilled < 10000)
            {
                killCounter.text = "0" + totalKilled.ToString();
            }
        }
    }
    public void increaseTotalSpawned()
    {
        totalSpawned++;
    }
    public void increaseTotalKilled()
    {
        totalKilled++;
    }
    public void increaseTotalGems(GameObject gem)
    {
        totalGems++;
        gems.Add(gem);
    }
    public void decreaseTotalGems(GameObject gem)
    {
        totalGems--;
        gems.Remove(gem);
    }
}
