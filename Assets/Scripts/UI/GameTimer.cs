using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public int minutes;
    public int seconds;
    public float timeStart = 0;
    public TMP_Text clock;
    public int maxTime = 10;
    GameManager manager;
    BGMusic soundManager;
    void Start()
    { 
        //updateBestTime();
        //soundManager = GameObject.Find("BGMusicManager").GetComponent<BGMusic>();
        soundManager = BGMusic.instance;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    bool switchEndgame;
    void Update()
    {
        if(minutes < maxTime)
            TimeUpdate();
        else if(minutes >= maxTime)
        {
            SceneManager.LoadScene(3);

            //end game
            //Debug.Log("Timer Max");
            //switchEndgame = true;
            //minutes = 0;

            //Time.timeScale = 0;
        }
        if (switchEndgame)
        {
            StartCoroutine(Win());

            switchEndgame = false;
        }

    }
    private void TimeUpdate()
    {
        timeStart += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeStart / 60);
        seconds = Mathf.FloorToInt(timeStart % 60);
        manager.minutes = minutes;
        manager.seconds = seconds;
        if(minutes >= 10)
            clock.text = (seconds > 9 ? minutes.ToString() + ":" + seconds.ToString() : minutes.ToString() + ":0" + seconds.ToString());
        else
            clock.text = $"0" + (seconds > 9 ? minutes.ToString() + ":" + seconds.ToString() : minutes.ToString() + ":0" + seconds.ToString());
        
    }
    public IEnumerator Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundManager.switchToWinMusic();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}
