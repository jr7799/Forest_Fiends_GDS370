using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class FileDataHandler
{
    private string datadDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "Trace";
    public FileDataHandler(string datadDirPath, string dataFileName, bool useEncryption)
    {
        this.useEncryption = useEncryption;
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
                //optionally encrypt data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
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

            //optionally encrypt data
            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

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
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
