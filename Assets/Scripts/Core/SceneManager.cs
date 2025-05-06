using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    static Scenes instance;
    GameManager gameManager;
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
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        if (gameManager.readyToPlay)
            SceneManager.LoadScene(1);
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
    public void Options()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
