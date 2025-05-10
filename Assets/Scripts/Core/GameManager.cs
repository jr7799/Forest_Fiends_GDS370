
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour, IDataPersistance
{
    [Header("Save Vars")]
    public int coins;
    SerializeDictionary<string, bool> unlocks = new SerializeDictionary<string, bool>();
    [Header("Stats")]
    public int totalKilled;
    public int minutes;
    public int seconds;
    private int totalSpawned;
    private TMP_Text killCounter;
    [Header("Gem Management")]
    public List<GameObject> gems = new List<GameObject>();
    public bool redGemSpawned = false;
    private int totalGems;
    [Header("GamePlay")]
    public bool readyToPlay;
    public bool characterSelected;
    static GameManager instance;
    [Header("Player Items")]
    public Sprite playerSprite;
    public RuntimeAnimatorController playerAnimation;
    public string playerWeapon;
    [Header("Level Select")]
    public int levelIndex = 0;

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
        characterSelected = false;
        
    }
    public void LoadData(GameData data)
    {
        coins = data.coins;
        
    }
    public void SaveData(GameData data)
    {
        data.coins = coins;
    }
    private void Update()
    {        
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "ForestGameScene")
        {
            if (killCounter == null)
                killCounter = GameObject.Find("KilledTXT").GetComponent<TMP_Text>();
        }
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
