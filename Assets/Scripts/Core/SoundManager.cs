using System.Drawing;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public AudioSource walkSource;
    [SerializeField] public AudioSource enemyDamageSource;
    [SerializeField] public AudioSource levelUpSource;
    [SerializeField] public AudioSource clickSource;
    [SerializeField] public AudioSource shootSource = new AudioSource();
    [SerializeField] public AudioSource loseSource;
    [SerializeField] public AudioSource winSource;
    [SerializeField] public AudioSource collectSource;
    [SerializeField] public AudioSource musicSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip backgroundMusic3;
    [SerializeField] private AudioClip enemyDamageSound;
    [SerializeField] private AudioClip levelUpMusic;
    [SerializeField] private AudioClip mouseClickSound;
    [SerializeField] private AudioClip playerShootingSound;
    [SerializeField] private AudioClip playerWalkingSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip collectSound;

    void Start()//
    {
       // PlayBackgroundMusic();
    }

    public void ButtonClick()
    {
        clickSource.PlayOneShot(mouseClickSound);
        
    }

    public void Shoot()//
    {
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

    public void playerDamaged()//
    {
        enemyDamageSource.PlayOneShot(enemyDamageSound);
    }

    public void CollecGem()//
    {
        collectSource.PlayOneShot(collectSound);
    }
    public void Win()//
    {
        winSource.PlayOneShot(winSound);
    }
    public void Lose()//
    {
        loseSource.PlayOneShot(loseSound);
    }
    public void LevelUpMusic()//
    {
        levelUpSource.PlayOneShot(levelUpMusic);
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    public void PlayBackgroundMusic()//
    {
        musicSource.clip = backgroundMusic3;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopBackgroundMusic()//
    {
        musicSource.Stop();
    }
    public void PlayerWalking()
    {
        walkSource.PlayOneShot(playerWalkingSound);
    }
    public void stopWalking()
    {
        walkSource.Stop();
    }
}
