using System.Drawing;
using UnityEngine;

public class SoundManager : MonoBehaviour, IDataPersistance
{
    [Header("Game Audio Sources")]
    [SerializeField] public AudioSource enemyDamageSource;
    [SerializeField] public AudioSource levelUpSource;
    [SerializeField] public AudioSource clickSource;
    [SerializeField] public AudioSource collectSource;
    [SerializeField] public AudioSource coinSource;
    [SerializeField] public AudioSource musicSource;

    [Header("Weapon  Audio Sources")]
    [SerializeField] public AudioSource shootSource = new AudioSource();
    [SerializeField] public AudioSource whipSource = new AudioSource();
    [SerializeField] public AudioSource starSource = new AudioSource();
    [SerializeField] public AudioSource trapSource = new AudioSource();
    [SerializeField] public AudioSource boomerangeSource = new AudioSource();

    [Header("Game Sound Effects")]
    [SerializeField] private AudioClip enemyDamageSound;
    [SerializeField] private AudioClip playerDamageSound;
    [SerializeField] private AudioClip levelUpMusic;
    [SerializeField] private AudioClip mouseClickSound;
    [SerializeField] private AudioClip collectSound;

    [Header("Weapon  Sound Effects")]
    [SerializeField] private AudioClip playerShootingSound;
    [SerializeField] private AudioClip whipSound;
    [SerializeField] private AudioClip starSound;
    [SerializeField] private AudioClip trapSound;
    [SerializeField] private AudioClip boomerangSound;
    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] public float volume = 1f;

    void Start()//
    {
    }
    private void Update()
    {
        musicSource.volume = volume;
        enemyDamageSource.volume = volume;
        levelUpSource.volume = volume;
        clickSource.volume = volume;
        shootSource.volume = volume;
        collectSource.volume = volume;
        coinSource.volume = volume;
        whipSource.volume = volume;
        starSource.volume = volume;
        trapSource.volume = volume;
        boomerangeSource.volume = volume;

    }
    public void ButtonClick()
    {
        clickSource.PlayOneShot(mouseClickSound, volume);
    } 
    public void enemyDamaged()//
    {
        enemyDamageSource.PlayOneShot(enemyDamageSound, volume);
    }
    public void CollecGem()//
    {
        collectSource.PlayOneShot(collectSound, volume);
    }
    public void LevelUpMusic()//
    {
        levelUpSource.PlayOneShot(levelUpMusic, volume);
    }
    #region Weapon Sounds
    public void Shoot()//
    {
        shootSource.volume = volume;
        shootSource.Play();
    }
    public void Whip()
    {
        whipSource.volume = volume;
        whipSource.Play();
    }
    public void Star()
    {
        starSource.volume = volume;
        starSource.Play();
    }
    public void caltrops()
    {
        trapSource.volume = volume;
        trapSource.Play();
    }
    public void boomerang()
    {
        boomerangeSource.volume = volume;
        boomerangeSource.Play();
    }
    public void coin()
    {
        coinSource.volume = volume;
        coinSource.Play();
    }
    public void LoadData(GameData data)
    {
        volume = data.SFXMusic;
    }

    public void SaveData(GameData data)
    {
        data.SFXMusic = volume;
    }
    #endregion
}
