using System.Drawing;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public AudioSource enemyDamageSource;
    [SerializeField] public AudioSource levelUpSource;
    [SerializeField] public AudioSource clickSource;
    [SerializeField] public AudioSource shootSource = new AudioSource();
    [SerializeField] public AudioSource collectSource;
    [SerializeField] public AudioSource musicSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip enemyDamageSound;
    [SerializeField] private AudioClip playerDamageSound;
    [SerializeField] private AudioClip levelUpMusic;
    [SerializeField] private AudioClip mouseClickSound;
    [SerializeField] private AudioClip playerShootingSound;
    [SerializeField] private AudioClip collectSound;

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

    public void StopBullet()//
    {
        shootSource.Stop();
    }
    public void Whip()
    {
        // Add whip sound if needed
    }
    public void Star()
    {
        // Add whip sound if needed
    }
    public void caltrops()
    {
        // Add whip sound if needed
    }
    public void boomerang()
    {

    }
    #endregion
}
