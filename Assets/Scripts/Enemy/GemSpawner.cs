using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    GameManager manager;

    [Header("Gem Spawn")]
    public GameObject smallGem;
    public GameObject mediumGem;
    public GameObject largeGem;
    public float roll;
    public float smallDropChance = 65;
    public float MediumDropChance = 5f;

    [Header("Red Gem Spawn")]
    public GameObject SpawnedRedGem;
    public int maxGemAmountBeforeLargeGem = 100;
    public int smallGemXP_Amount = 15;
    public int mediumGemXP_Amount = 25;

    [Header("Heals")]
    public float chanceOfHeal = 1f;
    public GameObject healPrefab;
    public float healOffset_X;
    public float healOffset_Y;
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        SpawnedRedGem = GameObject.Find("LargeGem(Clone)");
    }
    public void SpawnGem()
    {
        roll = Random.Range(0f, 100f);

        if (roll <= chanceOfHeal)
        {
            int posX = 0;
            int posY = 0;
            while(posX == 0)
                posX = Random.Range(-3, 3);
            while(posY == 0)
                posY = Random.Range(-3, 3);

            Instantiate(healPrefab, new Vector3(transform.position.x + posX, transform.position.y + posY, 0), Quaternion.identity);
        }

        if (roll <= smallDropChance && manager.gems.Count >= maxGemAmountBeforeLargeGem)
        {
            if (manager.redGemSpawned == false)
            {
                GameObject gem = Instantiate(largeGem, transform.position, Quaternion.identity);
                manager.redGemSpawned = true;
            }
            if (SpawnedRedGem != null)
            {
                SpawnedRedGem.GetComponent<GemXP>().XP_Amount += Random.value < 0.65f ? smallGemXP_Amount : mediumGemXP_Amount;
            }
        }
        else if (roll <= smallDropChance && roll >= MediumDropChance && manager.gems.Count < maxGemAmountBeforeLargeGem)
        {
            GameObject gem = Instantiate(smallGem, transform.position, Quaternion.identity);
            manager.increaseTotalGems(gem);
        }
        else if(roll <= MediumDropChance && manager.gems.Count < maxGemAmountBeforeLargeGem)
        {
            GameObject gem = Instantiate(mediumGem, transform.position, Quaternion.identity);
            manager.increaseTotalGems(gem);
        }
    }
}
