using System.Collections;
using ChristinaCreatesGames.EyeMovement;
using CodeMonkey.Utils;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth;
    float startHealth = 10f;
    public float minHealth = 8f;
    public float maxHealth = 13f;
    private GemSpawner gemSpawner;
    private PupilTracking pupilTracking;
    private GameManager manager;
    public SpriteRenderer spriteRenderer;
    SoundManager soundManager;

    void Start()
    {
        startHealth = Random.Range(minHealth, maxHealth);
        currentHealth = startHealth;
        pupilTracking = GameObject.Find("Viewcone").GetComponent<PupilTracking>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gemSpawner = GetComponent<GemSpawner>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    public void TakeDamage(float damage, bool isCriticalHit)
    {
        currentHealth -= damage;
        soundManager.playerDamaged();
        DamagePopup.Create(transform.position, damage, isCriticalHit);
        StartCoroutine(hitIndicator());
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public void OnDeath()
    {
        gemSpawner.SpawnGem();
        manager.increaseTotalKilled();
        if(gameObject.GetComponent<Eye>() != null)
            pupilTracking.RemoveFromEyes(gameObject.GetComponent<Eye>());
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        manager.gems.Remove(gameObject);
    }
    public IEnumerator hitIndicator()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
