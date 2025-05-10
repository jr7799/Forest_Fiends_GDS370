using NUnit.Framework;
using System.Collections.Generic;
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
    public WhipHitbox whipAttack;
    public PotionSplah potionSplash;
    public LobArcTween2D traps;
    public BoomerangLogic boomerang;
    public PlayerAttack playerAttack;

    [Header("Upgrade Button Groups")]
    public List<GameObject> upgradeButtonGroups = new List<GameObject>();
    public GameObject[] playerRelatedButtons;
    public GameObject[] allWeaponsButtons;
    public GameObject[] whipButtons;
    public GameObject[] boomButtons;
    public GameObject[] potionButtons;
    public GameObject[] trapButtons;
    public GameObject[] shootingButtons;
    public GameObject[] orbButtons;

    [Header("Weapon Button Groups")]
    public List<GameObject> weaponButtonGroups = new List<GameObject>();
    public GameObject whipUnlockButton;
    public GameObject boomUnlockButton;
    public GameObject potionUnlockButton;
    public GameObject trapUnlockButton;
    public GameObject shootingUnlockButton;
    public GameObject orbUnlockButton;

    private void Start()
    {
        pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        updateActiveButtonsUpgrades();
        updateActiveWeaponUnlockButtons();
    }
    private void Update()
    {
        updateActiveButtonsUpgrades();
        updateActiveWeaponUnlockButtons();
    }
    public void GetRandomButtonsWeapon(int count)
    {
        // Shuffle the list
        for (int i = 0; i < upgradeButtonGroups.Count; i++)
        {
            int randomIndex = Random.Range(i, upgradeButtonGroups.Count);
            GameObject temp = upgradeButtonGroups[i];
            upgradeButtonGroups[i] = upgradeButtonGroups[randomIndex];
            upgradeButtonGroups[randomIndex] = temp;
        }

        // Activate the first 'count' buttons, deactivate the rest
        for (int i = 0; i < upgradeButtonGroups.Count; i++)
        {
            upgradeButtonGroups[i].SetActive(i < count);
        }
    }
    public void GetRandomButtonsUpgrades(int count)
    {
        // Shuffle the list
        for (int i = 0; i < upgradeButtonGroups.Count; i++)
        {
            int randomIndex = Random.Range(i, upgradeButtonGroups.Count);
            GameObject temp = upgradeButtonGroups[i];
            upgradeButtonGroups[i] = upgradeButtonGroups[randomIndex];
            upgradeButtonGroups[randomIndex] = temp;
        }

        // Activate the first 'count' buttons, deactivate the rest
        for (int i = 0; i < upgradeButtonGroups.Count; i++)
        {
            upgradeButtonGroups[i].SetActive(i < count);
        }
    }
    private void updateActiveWeaponUnlockButtons()
    {
        if (!playerAttack.shootingActive && !weaponButtonGroups.Contains(shootingUnlockButton))
            weaponButtonGroups.Add(shootingUnlockButton);
        else if (playerAttack.shootingActive && weaponButtonGroups.Contains(shootingUnlockButton))
            weaponButtonGroups.Remove(shootingUnlockButton);

        if (!playerAttack.bibleActive && !weaponButtonGroups.Contains(orbUnlockButton))
            weaponButtonGroups.Add(orbUnlockButton);
        else if (playerAttack.bibleActive && weaponButtonGroups.Contains(orbUnlockButton))
            weaponButtonGroups.Remove(orbUnlockButton);

        if (!playerAttack.whipActive && !weaponButtonGroups.Contains(whipUnlockButton))
            weaponButtonGroups.Add(whipUnlockButton);
        else if (playerAttack.whipActive && weaponButtonGroups.Contains(whipUnlockButton))
            weaponButtonGroups.Remove(whipUnlockButton);

        if (!playerAttack.potionActive && !weaponButtonGroups.Contains(potionUnlockButton))
            weaponButtonGroups.Add(potionUnlockButton);
        else if (playerAttack.potionActive && weaponButtonGroups.Contains(potionUnlockButton))
            weaponButtonGroups.Remove(potionUnlockButton);

        if (!playerAttack.bearTrapsActive && !weaponButtonGroups.Contains(trapUnlockButton))
            weaponButtonGroups.Add(trapUnlockButton);
        else if (playerAttack.bearTrapsActive && weaponButtonGroups.Contains(trapUnlockButton))
            weaponButtonGroups.Remove(trapUnlockButton);

        if (!playerAttack.boomerangActive && !weaponButtonGroups.Contains(boomUnlockButton))
            weaponButtonGroups.Add(boomUnlockButton);
        else if (playerAttack.boomerangActive && weaponButtonGroups.Contains(boomUnlockButton))
            weaponButtonGroups.Remove(boomUnlockButton);

    }
    private void updateActiveButtonsUpgrades()
    {
        if (playerAttack.shootingActive)
        {
            foreach (GameObject button in shootingButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
        if (playerAttack.bibleActive)
        {
            foreach (GameObject button in orbButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
        if (playerAttack.whipActive)
        {
            foreach (GameObject button in whipButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
        if (playerAttack.potionActive)
        {
            foreach (GameObject button in potionButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
        if (playerAttack.bearTrapsActive)
        {
            foreach (GameObject button in trapButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
        if (playerAttack.boomerangActive)
        {
            foreach (GameObject button in boomButtons)
            {
                upgradeButtonGroups.Add(button);
            }
        }
    }
    #region WEAPON Unlock RELATED
        public void GetWhip()
        {
            playerAttack.whipActive = true;
        }
        public void getBoomerang()
        {
            playerAttack.boomerangActive = true;
        }
        public void getPotions()
        {
            playerAttack.potionActive = true;
        }
        public void getShooting()
        {
            playerAttack.shootingActive = true;
        }
        public void GetBibles()
        {
            playerAttack.SpawnBibles();
            playerAttack.bibleActive = true;
        }
        public void GetBearTraps()
        {
            playerAttack.bearTrapsActive = true;
        }
    #endregion

    #region PLAYER RELATED
    public void IncreaseHealth()
        {
            pHealth.playerHealth += 0.2f;
        }
        public void IncreaseSpeed()
        {
            playerController.moveSpeed += 0.2f;
        }
    #endregion
    #region ALL OTHER UPGRADES RELATED
        #region ALL WEAPONS RELATED
            public void IncreaseDamageAll()
            {
                if (playerAttack.shootingActive)
                    playerBulletAttack.damage += 0.2f;
                if (playerAttack.bibleActive)
                    bibleAttack.damage += 0.2f;
                if (playerAttack.whipActive)
                    whipAttack.damage += 0.2f;
                if (playerAttack.potionActive)
                    potionSplash.damage += 0.2f;
                if (playerAttack.bearTrapsActive)
                    traps.damage += 0.2f;
                if (playerAttack.boomerangActive)
                    boomerang.damage += 0.2f;
            }
            public void DecreaseCooldownTime()
            {
                if (playerAttack.shootingActive)
                    playerAttack.fireRate -= 0.02f;
                if (playerAttack.bibleActive)
                    playerAttack.bibleTimerReset -= 0.02f;
                if (playerAttack.whipActive)
                    playerAttack.whipTimerReset -= 0.02f;
                if (playerAttack.potionActive)
                    playerAttack.potionTimerReset -= 0.02f;
                if (playerAttack.bearTrapsActive)
                    playerAttack.whipTimerReset -= 0.02f;
                if (playerAttack.boomerangActive)
                    playerAttack.boomerangTimerReset -= 0.02f;
            }
            public void IncreaseNumOfProjectilesAllRelated()
            {
                playerAttack.numberOfBibles++;
                playerAttack.maxBoomerangs++;
                playerAttack.maxPotions++;
                playerAttack.maxTraps++;
            }
            public void IncreaseAllWeaponSpeeds()
            {
                playerAttack.bulletSpeed += 0.2f;
                playerAttack.orbitSpeed += 0.2f;
                playerAttack.boomerangSpeed += 0.2f;
                playerAttack.potionSpeed += 0.2f;
            }
            public void IncreaseAllTriggerSpeeds()
            {
                playerAttack.fireRate += 0.01f;
                playerAttack.whipTriggerSpeed += 0.2f;
                playerAttack.trapTriggerSpeed += 0.2f;
                playerAttack.potionTriggerSpeed += 0.2f;
                playerAttack.boomerangTriggerSpeed += 0.2f;
                playerAttack.bibleTriggerSpeed += 0.2f;
            }
    #endregion
        #region WHIP RELATED
        #endregion
    #endregion


}
