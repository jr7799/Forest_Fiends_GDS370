using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip winMusic; 
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioClip forestMusic;
    [SerializeField] private AudioClip desertMusic;
    [SerializeField] private AudioClip tundraMusic;

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
        switchToMainMenuMusic();
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopBackgroundMusic()//
    {
        musicSource.Stop();
    }
    public void switchToLoseMusic()
    {
        musicSource.clip = loseMusic;
    }
    public void switchToWinMusic()
    {
        musicSource.clip = winMusic;
    }
    public void switchToMainMenuMusic()
    {
        musicSource.clip = mainMenuMusic;

    }
    public void SwitchToForestMusic()
    {
        musicSource.clip = forestMusic;
    }
    public void SwitchToDesertMusic()
    {
        musicSource.clip = desertMusic;
    }
    public void SwitchToTundraMusic()
    {
        musicSource.clip = tundraMusic;
    }
}
