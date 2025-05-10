using UnityEngine;

public class PlayerInitalizer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    PlayerAttack playerAttack;
    string playerWeapon;
    GameManager manager;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
        spriteRenderer.sprite = manager.playerSprite;
        animator.runtimeAnimatorController = manager.playerAnimation;
        playerWeapon = manager.playerWeapon;
        if(playerWeapon == "Shooting")
        {
            playerAttack.shootingActive = true;
            playerAttack.inventoryCount++;
        }
        else if(playerWeapon == "bearTrap")
        {
            playerAttack.bearTrapsActive = true;
            playerAttack.inventoryCount++;
        }
        else if(playerWeapon == "Bomb")
        {
            playerAttack.potionActive = true;
            playerAttack.inventoryCount++;
        }
        else if (playerWeapon == "Orbs")
        {
            playerAttack.bibleActive = true;
            playerAttack.inventoryCount++;
        }
        else if (playerWeapon == "Whip")
        {
            playerAttack.whipActive = true;
            playerAttack.inventoryCount++;
        }
        else if (playerWeapon == "boomerang")
        {
            playerAttack.boomerangActive = true;
            playerAttack.inventoryCount++;
        }
    }
}
