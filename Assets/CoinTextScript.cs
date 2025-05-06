using TMPro;
using UnityEngine;

public class CoinTextScript : MonoBehaviour
{
    GameManager gameManager;
    public TMP_Text coinCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinCount = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinCount != null)
        {
            if (gameManager.coins < 10)
            {
                coinCount.text = "00000" + gameManager.coins.ToString();
            }
            else if (gameManager.coins >= 10 && gameManager.coins < 100)
            {
                coinCount.text = "0000" + gameManager.coins.ToString();
            }
            else if (gameManager.coins >= 100 && gameManager.coins < 1000)
            {
                coinCount.text = "000" + gameManager.coins.ToString();
            }
            else if (gameManager.coins >= 1000 && gameManager.coins < 10000)
            {
                coinCount.text = "00" + gameManager.coins.ToString();
            }
            else if (gameManager.coins >= 10000 && gameManager.coins < 100000)
            {
                coinCount.text = "0" + gameManager.coins.ToString();
            }
        }
    }
}
