using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataPersistance : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName; 


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
    }
    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
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
        Debug.Log("Loaded Coin Count = " + gamedata.coins);

    }
    public void SaveGame()
    {
        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.SaveData(ref gamedata);
        }
        Debug.Log("Saved Coin Count = " + gamedata.coins);
        fileDataHandler.Save(gamedata);
    }

    public List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
