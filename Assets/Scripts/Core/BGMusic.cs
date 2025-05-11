using UnityEngine;

public class BGMusic : MonoBehaviour, IDataPersistance
{
    public static BGMusic instance;

    [Header("Audio Settings")]
    [SerializeField][Range(0f, .5f)] public float volume = .5f;

    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;

    [Header("Music Tracks")]
    [SerializeField] public AudioClip mainMenuMusic;
    [SerializeField] public AudioClip winMusic;
    [SerializeField] public AudioClip loseMusic;
    [SerializeField] public AudioClip forestMusic;
    [SerializeField] public AudioClip desertMusic;
    [SerializeField] public AudioClip tundraMusic;

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

    public void LoadData(GameData data)
    {
        volume = data.MusicVolume;
    }

    public void SaveData(GameData data)
    {
        data.MusicVolume = volume;
    }
}