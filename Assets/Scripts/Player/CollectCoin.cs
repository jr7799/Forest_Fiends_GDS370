using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    GameManager gameManager;
    public int amount;
    SoundManager soundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soundManager.coin();
            gameManager.coins += amount;
            Destroy(gameObject);
        }
    }
}
