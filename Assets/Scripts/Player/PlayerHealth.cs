using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float playerHealth = 100f;
    public float originalPlayerHealth = 100f;
    public GameObject playerBlood;

    [Header("Health Bar")]
    public GameObject healthBarObject;        // GameObject holding the health bar UI
    public Image healthBarFill;               // Image component with Fill type set to "Filled"
    public Transform player;                  // Player reference
    public bool takingDamage;
    private Color healthbarAlpha;

    SoundManager soundManager;
    BGMusic bgMusic;

    private void Start()
    {
        playerHealth = originalPlayerHealth;

        healthbarAlpha = healthBarFill.color;
        healthbarAlpha = new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 0);
        healthBarFill.color = healthbarAlpha;

        playerBlood.SetActive(false);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        bgMusic = GameObject.Find("BGMusicManager").GetComponent<BGMusic>();
    }

    private void Update()
    {
        if(playerHealth >= originalPlayerHealth)
        {
            playerHealth = originalPlayerHealth;
        }

        UpdateHealthBar();

        if(takingDamage)
        {
            playerBlood.SetActive(true);
            healthbarAlpha = Color.Lerp(new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 0), new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 1), 1f);
            healthBarFill.color = healthbarAlpha;
        }
        else
        {
            playerBlood.SetActive(false);
            healthbarAlpha = Color.Lerp(new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 1), new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 0), 1f);
            healthBarFill.color = healthbarAlpha;
        }
        if(playerHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }
    void UpdateHealthBar()
    {
        if (healthBarObject == null || healthBarFill == null || player == null)
            return;

        // Update fill amount based on health
        float fill = playerHealth / originalPlayerHealth;
        healthBarFill.fillAmount = fill;
    }

    public void TakeDamage(float damage)
    {
        
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, originalPlayerHealth);
    }

    public void Heal(float amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, originalPlayerHealth);
    }
    public IEnumerator Death()
    {
        soundManager.Lose();
        bgMusic.StopBackgroundMusic();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
