using System.Collections.Generic;
using UnityEngine;

public class GemXP : MonoBehaviour
{
    public int XP_Amount;
    GameManager gameManager;
    [SerializeField] bool isRedGem;
    SoundManager soundManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager = SoundManager.instance;
        if (gameObject.name == "LargeGem")
        {
            isRedGem = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerXP playerXP = other.gameObject.GetComponent<PlayerXP>();
            if (playerXP != null)
            {
                soundManager.CollecGem();
                playerXP.AddXP(XP_Amount);
                if(!isRedGem)
                    gameManager.decreaseTotalGems(gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        if(isRedGem)
        {
            gameManager.redGemSpawned = false;
        }
    }
}
