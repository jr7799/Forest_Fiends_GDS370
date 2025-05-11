using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount;
    public GameObject playerHealthBar;
    private Color healthbarAlpha;
    public bool takingDamage;
    public Animator anim;
    public string animAttackName;
    public string animWalkName;

    public bool attacking;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (attacking)
        {
            if (anim != null)
            {

                anim.SetBool(animWalkName, false);
                anim.SetBool(animAttackName, true);
            }
        }
        else
        {
            if (anim != null)
            {

                anim.SetBool(animWalkName, true);
                anim.SetBool(animAttackName, false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                playerHealth.takingDamage = true;

            }
            attacking = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

            }
            attacking = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.takingDamage = false;
            attacking = false;
        }
    }
   
}