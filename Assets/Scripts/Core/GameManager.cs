using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistance
{
    [Header("Save Vars")]
    public int coins;

    [Header("Stats")]
    public int totalSpawned;
    public int totalKilled;
    public int minutes;
    public int seconds;
    public TMP_Text killCounter;
    [Header("Gem Management")]
    public int totalGems;
    public List<GameObject> gems = new List<GameObject>();
    public bool redGemSpawned = false;

    [Header("GamePlay")]
    static GameManager instance;
    public bool readyToPlay;

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
    private void Start()
    {
        readyToPlay = false;
    }
    public void LoadData(GameData data)
    {
        coins = data.coins;
    }
    public void SaveData(ref GameData data)
    {
        data.coins = coins;
    }
    private void Update()
    {        
        if(killCounter == null)
            killCounter = GameObject.Find("KilledTXT").GetComponent<TMP_Text>();

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
