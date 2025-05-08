using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

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
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
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

    }
    public void SaveGame()
    {
        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.SaveData(gamedata);
        }
        Debug.Log("Saved Coin Count = " + gamedata.coins);
        Debug.Log("Saved Unlocks = " + gamedata.charactersUnlocked.Keys);
        fileDataHandler.Save(gamedata);
    }

    public List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
