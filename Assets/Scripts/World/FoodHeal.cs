using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoodHeal : MonoBehaviour
{
    public float healAmount;
    public GameObject playerHealthBar;
    private Color healthbarAlpha;
    public float popupTime = 2;
    PlayerHealth pHealth;
    private void Start()
    {
        playerHealthBar = GameObject.Find("PlayerHealthBar");
        healthbarAlpha = playerHealthBar.GetComponent<Image>().color;
        pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if(pHealth.playerHealth < pHealth.originalPlayerHealth)
                {
                    playerHealth.Heal(healAmount);
                    StartCoroutine(healthBarPopUP());
                }
            }
            Destroy(gameObject);
        }
    }
    IEnumerator healthBarPopUP()
    {
        healthbarAlpha = new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 1);
        playerHealthBar.GetComponent<Image>().color = healthbarAlpha;
        yield return new WaitForSeconds(popupTime);
        if(!pHealth.takingDamage)
        {
            healthbarAlpha = new Color(healthbarAlpha.r, healthbarAlpha.g, healthbarAlpha.b, 0);
            playerHealthBar.GetComponent<Image>().color = healthbarAlpha;
        }
    }
}
