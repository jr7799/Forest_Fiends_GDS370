using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    static Scenes instance;
    GameManager gameManager;
    public int levelIndexSelected;
    BGMusic music;
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
        music = GameObject.Find("BGMusicManager").GetComponent<BGMusic>();
        levelIndexSelected = gameManager.levelIndex;
    }
    private void Update()
    {
        if(gameManager.levelIndex == 1 || gameManager.levelIndex == 8 || gameManager.levelIndex == 9)
            levelIndexSelected = gameManager.levelIndex;
    }
    public void MainMenu()
    {
        if (music.musicSource.clip != music.mainMenuMusic)
            music.switchToMainMenuMusic();
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        if (gameManager.readyToPlay)
        {
            if(levelIndexSelected == 1)
            {
                music.SwitchToForestMusic();
            }
            else if(levelIndexSelected == 8)
            {
                music.SwitchToDesertMusic();
            }
            else if(levelIndexSelected == 9)
            {
                music.SwitchToTundraMusic();
            }
            SceneManager.LoadScene(levelIndexSelected);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
            Debug.Log("Character Locked: not ready to play");
            //message or something
    }
    public void ToShop()
    {
        if(music.musicSource.clip != music.mainMenuMusic)
            music.switchToMainMenuMusic();
        SceneManager.LoadScene(5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
