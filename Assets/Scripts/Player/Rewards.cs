using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    public Magnet magnet;
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
        foreach (GameObject button in playerRelatedButtons)
        {
            if (!upgradeButtonGroups.Contains(button))
                upgradeButtonGroups.Add(button);
        }
        foreach (GameObject button in allWeaponsButtons)
        {
            if (!upgradeButtonGroups.Contains(button))
                upgradeButtonGroups.Add(button);
        }
    }
    private void Update()
    {
        updateActiveButtonsUpgrades();
        updateActiveWeaponUnlockButtons();
    }
    #region SETTING/ACTIVATING BUTTON RELATED
        public void GetRandomButtonsWeapon(int count)
        {
            List<GameObject> shuffled = new List<GameObject>(weaponButtonGroups);

            // Shuffle using Fisher-Yates
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                GameObject temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            // Activate the selected buttons, deactivate the rest
            for (int i = 0; i < weaponButtonGroups.Count; i++)
            {
            weaponButtonGroups[i].SetActive(shuffled.Take(count).Contains(weaponButtonGroups[i]));
            }
        }
        public void GetRandomButtonsUpgrades(int count)
        {
            // Create a copy to avoid modifying the original list order
            List<GameObject> shuffled = new List<GameObject>(upgradeButtonGroups);

            // Shuffle using Fisher-Yates
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                GameObject temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }
       
            // Activate the selected buttons, deactivate the rest
            for (int i = 0; i < upgradeButtonGroups.Count; i++)
            {
                upgradeButtonGroups[i].SetActive(shuffled.Take(count).Contains(upgradeButtonGroups[i]));
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
                    if(!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
            if (playerAttack.bibleActive)
            {
                foreach (GameObject button in orbButtons)
                {
                    if (!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
            if (playerAttack.whipActive)
            {
                foreach (GameObject button in whipButtons)
                {
                    if (!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
            if (playerAttack.potionActive)
            {
                foreach (GameObject button in potionButtons)
                {
                    if (!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
            if (playerAttack.bearTrapsActive)
            {
                foreach (GameObject button in trapButtons)
                {
                    if (!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
            if (playerAttack.boomerangActive)
            {
                foreach (GameObject button in boomButtons)
                {
                    if (!upgradeButtonGroups.Contains(button))
                        upgradeButtonGroups.Add(button);
                }
            }
        }
    #endregion

    #region WEAPON Unlock RELATED
        public void GetWhip()
        {
            playerAttack.whipActive = true;
            playerAttack.inventoryCount++;
        }
        public void getBoomerang()
        {
            playerAttack.boomerangActive = true; 
            playerAttack.inventoryCount++;
        }
        public void getPotions()
        {
            playerAttack.potionActive = true;
            playerAttack.inventoryCount++;
        }
        public void getShooting()
        {
            playerAttack.shootingActive = true;
            playerAttack.inventoryCount++;
        }
        public void GetBibles()
        {
            playerAttack.SpawnBibles();
            playerAttack.bibleActive = true;
            playerAttack.inventoryCount++;
        }
        public void GetBearTraps()
        {
            playerAttack.bearTrapsActive = true;
            playerAttack.inventoryCount++;
        }
    #endregion

    #region PLAYER RELATED
        public void IncreaseHealth()
        {
            pHealth.playerHealth += 0.2f;
        }
        public void IncreaseSpeed()
        {
            if(playerController.moveSpeed < 10)
                playerController.moveSpeed += 0.1f;
        }
        public void IncreaseMagnetRadius()
        {
            if(magnet.pullSpeed < 10)
                magnet.pullSpeed += 0.2f;
            if(magnet.magnetRadius < 20)
                magnet.magnetRadius += 0.2f;
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
            public void DecreaseCooldownTime() //decrase timer time
            {
                if (playerAttack.shootingActive)
                    playerAttack.fireRate += 0.02f;
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
            public void IncreaseAllTriggerSpeeds() // increase rate at which timer expires
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
            public void WHipDamageIncrease()
            {
                whipAttack.damage += 0.2f;
            }
            public void WhipTriggerIncrease() //
            {
                playerAttack.whipTriggerSpeed += 0.2f;
            }
            public void WhipTimerDecrease()
            {
                playerAttack.whipTimerReset -= 0.02f;
            }
        #endregion
        #region SHOOTING RELATED
            public void ShootingDamageIncrease()
            {
                playerBulletAttack.damage += 0.2f;
            }
            public void ShootingTriggerIncrease() //
            {
                playerAttack.fireRate += 0.01f;
            }
        #endregion
        #region BIBLE RELATED
            public void BibleDamageIncrease()
            {
                bibleAttack.damage += 0.2f;
            }
            public void BibleTriggerIncrease() //
            {
                playerAttack.bibleTriggerSpeed += 0.2f;
            }
            public void BibleTimerDecrease()
            {
                playerAttack.bibleTimerReset -= 0.02f;
            }
            public void IncreaseBibleNumber()
            {
                playerAttack.numberOfBibles++;
            }
            public void IncreaseBibleRotateSpeed()
            {
                playerAttack.orbitSpeed += 0.2f;
            }
        #endregion
        #region POTION RELATED
            public void PotionDamageIncrease()
            {
                potionSplash.damage += 0.2f;
            }
            public void PotionTriggerIncrease() //
            {
                playerAttack.potionTriggerSpeed += 0.2f;
            }
            public void PotionTimerDecrease()
            {
                playerAttack.potionTimerReset -= 0.02f;
            }
            public void IncreasePotionNumber()
            {
                playerAttack.maxPotions++;
            }
            public void IncreasePotionSpeed()
            {
                playerAttack.potionSpeed += 0.2f;
            }
        #endregion
        #region BOOMERANG RELATED
            public void BoomerangeIncreaseBounce()
            {
                playerAttack.boomerangMaxTargets++;
            }
            public void BoomerangeDamageIncrease()
            {
                boomerang.damage += 0.2f;
            }
            public void BoomerangeTriggerIncrease() //
            {
                playerAttack.boomerangTriggerSpeed += 0.2f;
            }
            public void BoomerangeTimerDecrease()
            {
                playerAttack.boomerangTimerReset -= 0.02f;
            }
            public void IncreasBoomerangeNumber()
            {
                playerAttack.maxBoomerangs++;
            }
            public void IncreaseBoomerangeSpeed()
            {
                playerAttack.boomerangSpeed += 0.2f;
            }
        #endregion
        #region TRAPS/CALTROP RELATED
            public void TrapsDamageIncrease()
            {
                traps.damage += 0.2f;
            }
            public void TrapsTriggerIncrease() //
            {
                playerAttack.trapTriggerSpeed += 0.2f;
            }
            public void TrapsTimerDecrease()
            {
                playerAttack.trapTimerReset -= 0.02f;
            }
            public void IncreaseTrapsNumber()
            {
                playerAttack.maxTraps++;
            }
        #endregion
    #endregion


}
