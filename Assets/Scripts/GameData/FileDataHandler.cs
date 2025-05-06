using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class FileDataHandler
{
    private string datadDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string datadDirPath, string dataFileName)
    {
        this.datadDirPath = datadDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(datadDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //deserialize
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(datadDirPath, dataFileName);
        try
        {
            //create directory path
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //serialize to json string
            string dataToStore = JsonUtility.ToJson(data, true);
            //write file to system
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}
