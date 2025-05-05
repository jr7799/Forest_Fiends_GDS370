using UnityEngine;

public class BGMusic : MonoBehaviour
{
    static BGMusic instance;
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioClip backgroundMusic3;

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
        PlayBackgroundMusic();
    }
    public void StopBackgroundMusic()//
    {
        musicSource.Stop();
    }
    public void PlayBackgroundMusic()//
    {
        musicSource.clip = backgroundMusic3;
        musicSource.loop = true;
        musicSource.Play();
    }
}
