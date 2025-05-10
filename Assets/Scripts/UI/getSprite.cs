using UnityEngine;

public class getSprite : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gameManager.playerSprite;
    }
}
