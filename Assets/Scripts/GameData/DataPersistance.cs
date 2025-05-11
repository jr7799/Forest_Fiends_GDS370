using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class DataPersistance : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName; 
    [SerializeField] private bool useEncryption; 


    private GameData gamedata;
    public List<IDataPersistance> dataPersistanceObjects;
    public static DataPersistance instance { get; private set; }

    private FileDataHandler fileDataHandler;
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
        string exeFolder = Path.GetDirectoryName(Application.dataPath);

        // Define the saves folder path
        string savesFolder = Path.Combine(exeFolder, "Saves");
        this.fileDataHandler = new FileDataHandler(savesFolder, fileName, useEncryption);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void NewGame()
    {
        this.gamedata = new GameData();
    }
    public void LoadGame()
    {
        gamedata = fileDataHandler.Load();
        if(gamedata == null)
        {
            Debug.Log("No Data found. Default Initalized");
            NewGame();
        }
        foreach(IDataPersistance obj in dataPersistanceObjects)
        {
            obj.LoadData(gamedata);
        }
        Debug.Log("Loaded Unlocks = " + gamedata.charactersUnlocked.Keys);
        Debug.Log("Loaded Coin Count = " + gamedata.coins);
        Debug.Log("Loaded Damage Numbers is On: " + gamedata.damageNumbersOn);
        Debug.Log("Loaded SFX Volume: " + gamedata.SFXMusic);
        Debug.Log("Loaded Music Volume: " + gamedata.MusicVolume);

    }
    public void SaveGame()
    {
        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.SaveData(gamedata);
        }
        Debug.Log("Saved Coin Count = " + gamedata.coins);
        Debug.Log("Saved Unlocks = " + gamedata.charactersUnlocked.Keys); 
        Debug.Log("Saved Damage Numbers is On: " + gamedata.damageNumbersOn);
        Debug.Log("Saved SFX Volume: " + gamedata.SFXMusic);
        Debug.Log("Saved Music Volume: " + gamedata.MusicVolume);
        fileDataHandler.Save(gamedata);
    }

    public List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
