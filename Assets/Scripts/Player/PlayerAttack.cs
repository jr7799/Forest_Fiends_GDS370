using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    [Header("Core Items")]
    SoundManager soundManager; 

    [Header("Unlock Bools")]
    public bool whipActive = false;
    public bool bibleActive = false;
    public bool boomerangActive = false;
    public bool shootingActive = false;
    public bool bearTrapsActive = false;
    public bool potionActive = false;

    [Header("ScreenShake")]
    private ScreenShake screenShake;
    private float magnitude = 0.2f;
    private float duration = 0.2f;
    private Camera cam;

    [Header("'inventory'")]
    public int inventoryCount;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform tempPlayerWeapon;
    public float bulletSpeed = 10f;
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    [Header("Bible Orbit Settings")]
    public GameObject biblePrefab;
    public int numberOfBibles = 3;
    public float orbitRadius = 2f;
    public float orbitSpeed = 100f;
    public float bibleOrbitDuration = 5f;
    public float fadeDuration = 2f;
    public float bibleResetInterval = 10f;
    public float bibleSpawnDecrement = 0.2f;
    private GameObject[] bibles;
    private float orbitTimer;
    private bool isFading = false;
    private Coroutine fadeCoroutine;

    [Header("Bible Timer")]
    public float bibleTimer = 0;
    public float bibleTimerReset = 15;
    public float bibleTriggerSpeed = 0.5f; //reward increase to trugger faster

    [Header("Whip Settings")]       
    public GameObject whipParticlePrefab;   // New whip particle
    public Transform whipSpawnPoint;        // Where to spawn the particle
    public float whipSpawnDecrement = 0.15f;

    [Header("Whip Timer")]
    public float whipTimer = 0;
    public float whipTimerReset = 12;
    public float whipTriggerSpeed = 0.5f; //reward increase to trigger faster

    [Header("Boomerang Settings")]
    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;
    public float boomerangSpeed = 10f;
    public float boomerangReturnSpeed = 15f;
    public float boomerangMaxDistance = 10f;
    public float boomerangChainRadius = 5f;
    public int boomerangMaxTargets = 5;
    public int maxBoomerangs = 1; // Set in Inspector
    GameObject boomerangObj;
    public List<GameObject> activeBoomerangs = new List<GameObject>();
    public GameObject[] enemies;
    public Vector3[] enemyPositions;

    [Header("Boomerang Timer")]
    public float boomerangTimer = 0;
    public float boomerangTimerReset = 10;
    public float boomerangTriggerSpeed = 0.5f;

    [Header("Throwable Potion Settings")]
    public GameObject potionPrefab;
    public float potionLobHeight = 5f;
    public float potionLobDuration = 0.5f;
    public float potionImpactRadius = 3f;
    public int maxPotions = 3;
    public float minRadius = 2f;
    public float maxRadius = 10f;
    public float spinSpeed = 360f; // degrees per second
    public float potionSpeed = 10f;
    public float distanceThreshold = 0.1f;
    public float potionDistance = 10f;
    GameObject potion;
    public List<GameObject> activePotions = new List<GameObject>();

    [Header("Throwable Potion Timer")]
    public float potionTimer = 0;
    public float potionTimerReset = 15;
    public float potionTriggerSpeed = 0.5f;

    [Header("Bear Trap Settings")]
    public int maxTraps = 3;
    public GameObject bearTrapPrefab;
    public float bearTrapThrowForce = 10f;
    public float bearTrapCooldown = 2f;
    private float lastBearTrapTime = -Mathf.Infinity;
    GameObject trap;
    private List<GameObject> activeTraps = new List<GameObject>();

    [Header("Bear trap Timer")]
    public float trapTimer = 0;
    public float trapTimerReset = 10;
    public float trapTriggerSpeed = 0.5f;

    [Header("Inventory Images")]
    public HorizontalLayoutGroup images;
    public GameObject whipImage;
    public GameObject ArrowImage;
    public GameObject OrbImage;
    public GameObject caltropImage;
    public GameObject starImage;
    public GameObject BoomerangImage;
    private void Awake()
    {
        whipActive = false;
        bibleActive = false;
        boomerangActive = false;
        shootingActive = false;
        bearTrapsActive = false;
        potionActive = false;
    }
    void Start()
    {
        cam = Camera.main;
        screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        ArrowImage.SetActive(false);
        OrbImage.SetActive(false);
        whipImage.SetActive(false);
        BoomerangImage.SetActive(false);
        caltropImage.SetActive(false);
        starImage.SetActive(false);


        orbitTimer = bibleOrbitDuration;
    }

    void Update()
    {
        switch (inventoryCount)
        {
            case 1:
                images.spacing = 0;
                break;
            case 2:
                images.spacing = -51;
                break;
            case 3:
                images.spacing = -25;
                break;
            case 4:
                images.spacing = 0;
                break;
        }


        if (shootingActive)
        {
            ArrowImage.SetActive(true);
            HandleShooting();
        }
           
        if (bibleActive)
        {
            OrbImage.SetActive(true);
            HandleBibleLogic();
        }
        if (whipActive)
        {
            whipImage.SetActive(true);
            HandleWhipLogic();
        }
        if (boomerangActive)
        {
            BoomerangImage.SetActive(true);
            ThrowBoomerang();
        }
        if (bearTrapsActive)
        {
            caltropImage.SetActive(true);
            ThrowBearTrap();
        }
        if (potionActive)
        {
            starImage.SetActive(true);
            ThrowPotion();
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && Time.timeScale == 1)
        {
            soundManager.Shoot();
            nextFireTime = Time.time + fireRate;

            if (bulletPrefab != null && tempPlayerWeapon != null)
            {
                Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                Vector2 direction = (mouseWorldPos - tempPlayerWeapon.position).normalized;
                Vector2 playerPos = transform.position;
                Vector2 weaponPos = tempPlayerWeapon.position;
                Vector2 toWeapon = weaponPos - playerPos;
                Vector2 toMouse = (Vector2)mouseWorldPos - playerPos;
                // Check if the mouse is between player and weapon
                bool isMouseBetween = Vector2.Dot(toMouse.normalized, toWeapon.normalized) > 0.99f // very close to same direction
                                      && toMouse.magnitude < toWeapon.magnitude;

                // Calculate the rotation angle from the direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);

                GameObject bullet = Instantiate(bulletPrefab, tempPlayerWeapon.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    if(!isMouseBetween)
                        rb.linearVelocity = direction * bulletSpeed;
                    else
                    {
                        rb.linearVelocity = -direction * bulletSpeed;
                        bullet.GetComponent<SpriteRenderer>().flipY = true;
                    }
                }
                //screenShake.Shake(duration, magnitude);
            }
        }
    }
    void HandleBibleLogic()
    {
        bibleTimer -= Time.deltaTime * bibleTriggerSpeed;

        if (bibleTimer <= 0f)
        {
            if (bibles != null && !isFading)
            {
                RotateBibles();
                orbitTimer -= Time.deltaTime;

                if (orbitTimer <= 0)
                {
                    fadeCoroutine = StartCoroutine(FadeOutBibles());
                }
            }
            else if (bibles == null)
            {
                ResetBibleOrbit();
            }
        }
    }
    void HandleWhipLogic()
    {
        whipTimer -= Time.deltaTime * whipTriggerSpeed;

        if (whipTimer <= 0f)
        {
            TriggerWhipEffect();
        }       
    }
    void TriggerWhipEffect()
    {
        Debug.Log("Whip effect triggered");
        if (whipParticlePrefab != null && whipSpawnPoint != null)
        {
            Vector3 spawnPos = whipSpawnPoint.position;
            spawnPos.z = 0f;

            GameObject newWhip = Instantiate(whipParticlePrefab, spawnPos, Quaternion.identity);

            Transform hitbox = newWhip.transform.Find("WhipHitbox");
            if (hitbox != null)
                hitbox.gameObject.SetActive(true);

            //StartCoroutine(DisableWhipHitbox(hitbox?.gameObject, 0.3f));

            ParticleSystem ps = newWhip.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                Destroy(newWhip, ps.main.duration + ps.main.startLifetime.constant);
            }
            else
            {
                Destroy(newWhip, 2f);
            }
        }
        else
        {
            Debug.LogWarning("Missing whipParticlePrefab or whipSpawnPoint!");
        }
        whipTimer = whipTimerReset;
    }
    //IEnumerator DisableWhipHitbox(GameObject hitbox, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if (hitbox != null)
    //        hitbox.SetActive(false);
    //}
    public void SpawnBibles()
    {
        bibles = new GameObject[numberOfBibles];
        for (int i = 0; i < numberOfBibles; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfBibles;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitRadius;
            bibles[i] = Instantiate(biblePrefab, transform.position + pos, Quaternion.identity);
            bibles[i].transform.parent = transform;
        }

        orbitTimer = bibleOrbitDuration;
        isFading = false;
    }
    IEnumerator FadeOutBibles()
    {
        isFading = true;
        float elapsed = 0f;
        SpriteRenderer[] renderers = new SpriteRenderer[numberOfBibles];

        for (int i = 0; i < bibles.Length; i++)
        {
            if (bibles[i] != null)
                renderers[i] = bibles[i].GetComponent<SpriteRenderer>();
        }

        while (elapsed < fadeDuration)
        {
            RotateBibles(); // Keep them spinning while fading

            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] != null)
                {
                    Color c = renderers[i].color;
                    c.a = alpha;
                    renderers[i].color = c;
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < bibles.Length; i++)
        {
            if (bibles[i] != null)
                Destroy(bibles[i]);
        }

        bibles = null;
        bibleTimer = bibleTimerReset;
        isFading = false;
    }
    void ResetBibleOrbit()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
        isFading = false;
        SpawnBibles();
    }
    void RotateBibles()
    {
        if (bibles == null) return;

        for (int i = 0; i < bibles.Length; i++)
        {
            float angle = Time.time * orbitSpeed + (i * 360f / bibles.Length);
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * orbitRadius;
            bibles[i].transform.localPosition = offset;
        }
    }
    void ThrowBoomerang()
    {
        boomerangTimer -= Time.deltaTime * boomerangTriggerSpeed;

        if (boomerangTimer <= 0f)
        {
            BoomerangLogic logic = null;

            if (boomerangPrefab != null)
            {
                for(int i = 0; i < maxBoomerangs; i++)
                {
                    GameObject newBoomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);
                    activeBoomerangs.Add(newBoomerang);

                    Vector3 targetPosition = GetNthClosestEnemyPosition(i);
                    Vector3 throwDirection = (targetPosition - boomerangSpawnPoint.position).normalized;

                    logic = newBoomerang.GetComponent<BoomerangLogic>();
                    if (logic == null) logic = newBoomerang.AddComponent<BoomerangLogic>();

                    logic.destroyed.AddListener(() =>
                    {
                        //boomerangeResetTimer();
                        activeBoomerangs.Remove(newBoomerang); // Remove from list on destroy
                    });

                    logic.Initialize(
                        transform,
                        boomerangSpeed,
                        boomerangReturnSpeed,
                        boomerangMaxDistance,
                        boomerangChainRadius,
                        boomerangMaxTargets,
                        throwDirection
                    );
                }
                boomerangTimer = boomerangTimerReset; // Reset the timer or however you wish
            }
            
        }
    }    
    public Vector3 GetNthClosestEnemyPosition(int n)//for boomerang
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 currentPosition = transform.position;

        // Sort enemies by distance
        var sortedEnemies = enemies
            .OrderBy(enemy => Vector3.Distance(currentPosition, enemy.transform.position))
            .ToList();

        // Return the Nth closest enemy position (0-based index)
        if (n >= 0 && n < sortedEnemies.Count)
        {
            return sortedEnemies[n].transform.position;
        }

        return Vector3.zero; // Return zero if index is out of range
    } 
    void ThrowPotion()
    {
        potionTimer -= Time.deltaTime * potionTriggerSpeed;
        if (potionTimer <= 0)
        {
            if(potionPrefab != null)
            {
                if (activePotions.Count < maxPotions)
                {
                    for (int i = 0; i < maxPotions; i++)
                    {
                        Vector2 start = transform.position;
                        Vector2 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        potion = Instantiate(potionPrefab, start, Quaternion.identity);
                        activePotions.Add(potion);
                        potion.GetComponent<PotionImpact>().destroyed.AddListener(() =>
                        {
                            activePotions.Remove(potion); // Remove from list on destroy
                        });

                        StartCoroutine(PotionLobArc(potion, new Vector2(potion.transform.position.x, potion.transform.position.y + potionDistance)));
                    }
                }
                else return;
            }
            potionTimer = potionTimerReset;
        }
        
    }
    IEnumerator PotionLobArc(GameObject potion, Vector2 arcTarget)
    {

        // === Phase 1: Move up to arc peak ===
        while (potion != null && Vector2.Distance(potion.transform.position, arcTarget) > distanceThreshold)
        {
            Vector2 currentPos = potion.transform.position;
            potion.transform.position = Vector2.MoveTowards(currentPos, arcTarget, potionSpeed/1.75f * Time.deltaTime);        
            potion.transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
            yield return null;
        }

        if (potion == null) yield break;

        potion.transform.position = arcTarget;
        Debug.Log("Potion reached arc peak at: " + arcTarget);

        // === Phase 2: Calculate random target around the player (this object) ===

        float randomRadius = Random.Range(minRadius, maxRadius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 randomOffset = randomDirection * randomRadius;
        Vector2 finalTarget = (Vector2)transform.position + randomOffset;

        Debug.DrawLine(arcTarget, finalTarget, Color.magenta, 1f);
        Debug.Log("Potion moving to final randomized target at: " + finalTarget);

        // === Phase 3: Move to final position around player ===
        while (potion != null && Vector2.Distance(potion.transform.position, finalTarget) > distanceThreshold)
        {
            Vector2 currentPos = potion.transform.position;
            potion.transform.position = Vector2.MoveTowards(currentPos, finalTarget, potionSpeed * Time.deltaTime);
            potion.transform.Rotate(0f, 0f, 0f);
            yield return null;
        }

        if (potion == null) yield break;
        potion.transform.position = finalTarget;
        potion.GetComponent<PotionImpact>().position = finalTarget;
        Destroy(potion);
        potionTimer = potionTimerReset;
    }
    void ThrowBearTrap()
    {
        trapTimer -= Time.deltaTime * trapTriggerSpeed;
        if(trapTimer <= 0f)
        {
            if (bearTrapPrefab == null) return;
            for (int i = 0; i < maxTraps; i ++)
            {
                trap = Instantiate(bearTrapPrefab, transform.position, Quaternion.identity);
            }
            trapTimer = trapTimerReset;
        }
        
    }

    // Helper to clamp direction to 4 cardinal directions
    Vector2 GetCardinalDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return new Vector2(Mathf.Sign(input.x), 0);
        else
            return new Vector2(0, Mathf.Sign(input.y));
    }
}

