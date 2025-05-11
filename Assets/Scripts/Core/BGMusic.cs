using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;

    [Header("Audio Settings")]
    [SerializeField][Range(0f, 1f)] public float volume = 1f;

    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;

    [Header("Music Tracks")]
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
        musicSource.volume = volume;
        switchToMainMenuMusic();
        musicSource.loop = true;
        musicSource.Play();
    }
    private void Update()
    {
        musicSource.volume = volume;
    }
    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void switchToLoseMusic()
    {
        musicSource.clip = loseMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void switchToWinMusic()
    {
        musicSource.clip = winMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void switchToMainMenuMusic()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void SwitchToForestMusic()
    {
        musicSource.clip = forestMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void SwitchToDesertMusic()
    {
        musicSource.clip = desertMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void SwitchToTundraMusic()
    {
        musicSource.clip = tundraMusic;
        musicSource.volume = volume;
        musicSource.Play();
    }
}