using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    static Scenes instance;
    GameManager gameManager;
    public int levelIndexSelected;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelIndexSelected = gameManager.levelIndex;
    }
    private void Update()
    {
        if(gameManager.levelIndex == 1 || gameManager.levelIndex == 8 || gameManager.levelIndex == 9)
            levelIndexSelected = gameManager.levelIndex;
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        if (gameManager.readyToPlay)
        {
            SceneManager.LoadScene(levelIndexSelected);

        }
        else
            Debug.Log("Character Locked: not ready to play");
            //message or something
    }
    public void ToShop()
    {
        SceneManager.LoadScene(5);
    }
    public void LoseScene()
    {
        SceneManager.LoadScene(2);
    }
    public void WinScene()
    {
        SceneManager.LoadScene(3);
    }
    public void Controls()
    {
        SceneManager.LoadScene(4);
    }
    public void Options()
    {
        SceneManager.LoadScene(6);
    }
    public void LevelSelect()
    {
        if (gameManager.characterSelected)
        {
            SceneManager.LoadScene(7);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
