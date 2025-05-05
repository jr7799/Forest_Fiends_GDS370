using UnityEngine;

public class Rewards : MonoBehaviour
{
    [Header("Health Scripts")]
    PlayerHealth pHealth;
    [Header("Movement Scripts")]
    PlayerController playerController;
    [Header("Damage Scripts")]
    public Bullet playerBulletAttack;
    public BibleOrbiter bibleAttack;
    public WhipAttack whipAttack;
    public PlayerAttack playerAttack;
    private void Start()
    {
        pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }
    public void IncreaseHealth()
    {
        pHealth.playerHealth += 0.2f;
    }
    public void IncreaseSpeed()
    {
        playerController.moveSpeed += 0.2f;
    }
    public void IncreaseDamageAll()
    {
        playerBulletAttack.damage += 0.2f;
        bibleAttack.damage += 0.2f;
        whipAttack.damage += 0.2f;

    }
    public void GetWhip()
    {
        playerAttack.whipActive = true;
    }
    public void GetBibles()
    {
        playerAttack.SpawnBibles();
        playerAttack.bibleActive = true;

    }
}
