using System.IO;
using UnityEngine;

public class SaveFolderCreator : MonoBehaviour
{
    void Start()
    {
        // Get the folder where the game executable is located
        string exeFolder = Path.GetDirectoryName(Application.dataPath);

        // Define the saves folder path
        string savesFolder = Path.Combine(exeFolder, "Saves");

        // Create the folder if it doesn't exist
        if (!Directory.Exists(savesFolder))
        {
            Directory.CreateDirectory(savesFolder);
            Debug.Log("Created Saves folder at: " + savesFolder);
        }
        else
        {
            Debug.Log("Saves folder already exists at: " + savesFolder);
        }
    }
}
