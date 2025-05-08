using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Net;


public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform tempPlayerWeapon;
    public float bulletSpeed = 10f;
    public float fireRate = 0.25f;

    [Header("Bible Orbit Settings")]
    public GameObject biblePrefab;
    public int numberOfBibles = 3;
    public float orbitRadius = 2f;
    public float orbitSpeed = 100f;
    public float bibleOrbitDuration = 5f;
    public float fadeDuration = 2f;
    public float bibleResetInterval = 10f;
    public float bibleSpawnDecrement = 0.2f;

    [Header("Whip Settings")]       
    public GameObject whipParticlePrefab;   // New whip particle
    public Transform whipSpawnPoint;        // Where to spawn the particle
    public float whipSpawnDecrement = 0.15f;

    [Header("ScreenShake")]
    public ScreenShake screenShake;
    public float magnitude = 0.2f;
    public float duration = 0.2f;

    private float nextFireTime = 0f;
    private Camera cam;

    private GameObject[] bibles;
    private float orbitTimer;
    private bool isFading = false;
    private Coroutine fadeCoroutine;

    private enum AttackState { Bible, Whip }
    //private AttackState currentState = AttackState.Bible;

    [Header("Whip Timer")]
    public float whipTimer = 0;
    public float whipTimerReset = 12;
    public float whipTriggerSpeed = 0.5f; //reward increase to trigger faster

    [Header("Bible Timer")]
    public float bibleTimer = 0;
    public float bibleTimerReset = 15;
    public float bibleTriggerSpeed = 0.5f; //reward increase to trugger faster

    [Header("Potion Timer")]
    public float potionTimer = 0;
    public float potionTimerReset = 15;
    public float potionTriggerSpeed = 0.5f;

    [Header("Boomerang Timer")]
    public float boomerangTimer = 0;
    public float boomerangTimerReset = 10;
    public float boomerangTriggerSpeed = 0.5f;
    [Header("Unlock Bools")]

    public bool whipActive = false;
    public bool bibleActive = false;
    public bool boomerangActive = false;
    public bool shootingActive = false;
    public bool bearTrapsActive = false;
    public bool potionActive = false;
    [Header("Boomerang Settings")]

    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;
    public float boomerangSpeed = 10f;
    public float boomerangReturnSpeed = 15f;
    public float boomerangMaxDistance = 10f;
    public float boomerangChainRadius = 5f;
    public int boomerangMaxTargets = 5;

    [Header("Throwable Potion Settings")]
    public GameObject potionPrefab;
    public float potionLobHeight = 5f;
    public float potionLobDuration = 0.5f;
    public float potionImpactRadius = 3f;

    [Header("Bear Trap Settings")]
    public GameObject bearTrapPrefab;
    public float bearTrapThrowForce = 10f;
    public float bearTrapCooldown = 2f;
    private float lastBearTrapTime = -Mathf.Infinity;

    SoundManager soundManager;
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

        orbitTimer = bibleOrbitDuration;
    }

    void Update()
    {
        if (shootingActive)
            HandleShooting();
        if (bibleActive)
            HandleBibleLogic();
        if (whipActive)
            HandleWhipLogic();
        if (boomerangActive)
            ThrowBoomerang();
        if (bearTrapsActive)
            Debug.Log("Traps Active");
        if (potionActive)
        {
            Debug.Log("BOmbs Active");
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
                        rb.linearVelocity = -direction * bulletSpeed;
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
            spawnPos.z = 0f; // force into 2D view

            GameObject newWhip = Instantiate(whipParticlePrefab, spawnPos, Quaternion.identity);

            ParticleSystem ps = newWhip.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                Destroy(newWhip, ps.main.duration + ps.main.startLifetime.constant); // cleanup after done
            }
            else
            {
                Destroy(newWhip, 2f); // fallback if no ParticleSystem
            }
        }
        else
        {
            Debug.LogWarning("Missing whipParticlePrefab or whipSpawnPoint!");
        }
        whipTimer = whipTimerReset;
    }
    IEnumerator DisableWhipEffectAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (whipParticlePrefab != null)
        {
            whipParticlePrefab.SetActive(false);
        }
    }
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
    GameObject boomerangObj;
    void ThrowBoomerang()
    {
        boomerangTimer -= Time.deltaTime * boomerangTriggerSpeed;
        if (boomerangTimer <= 0f)
        {
            BoomerangLogic logic = null;
            if (boomerangObj == null && boomerangPrefab != null)
            {
                boomerangObj = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);

                Vector3 targetPosition = GetClosestEnemyPosition();
                Vector3 throwDirection = (targetPosition - boomerangSpawnPoint.position).normalized;

                // Ensure BoomerangLogic is on the prefab or added here
                logic = boomerangObj.GetComponent<BoomerangLogic>();
                if (logic == null) logic = boomerangObj.AddComponent<BoomerangLogic>();
                logic.destroyed.AddListener(boomerangeResetTimer);
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
            else return;           
        }
    }
    void boomerangeResetTimer()
    {
        boomerangTimer = boomerangTimerReset;
    }
     
    public GameObject[] enemies;
    public Vector3[] enemyPositions;
    public Vector3 GetClosestEnemyPosition()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy != null ? closestEnemy.transform.position : Vector3.zero;
    }
    GameObject potion;
    void ThrowPotion()
    {
        potionTimer -= Time.deltaTime * potionTriggerSpeed;
        if (potionTimer <= 0)
        {
            if (potion == null && potionPrefab != null)
            {
                Vector2 start = transform.position;
                Vector2 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                potion = Instantiate(potionPrefab, start, Quaternion.identity);

                StartCoroutine(PotionLobArc(potion, new Vector2(potion.transform.position.x, potion.transform.position.y + potionDistance)));
            }
            else return;
        }
        
    }
    public float minRadius = 2f;
    public float maxRadius = 10f;
    public float spinSpeed = 360f; // degrees per second
    public float speed = 10f;
    public float distanceThreshold = 0.1f;
    public float potionDistance = 10f;
    IEnumerator PotionLobArc(GameObject potion, Vector2 arcTarget)
    {

        // === Phase 1: Move up to arc peak ===
        while (potion != null && Vector2.Distance(potion.transform.position, arcTarget) > distanceThreshold)
        {
            Vector2 currentPos = potion.transform.position;
            potion.transform.position = Vector2.MoveTowards(currentPos, arcTarget, speed/1.75f * Time.deltaTime);        
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
            potion.transform.position = Vector2.MoveTowards(currentPos, finalTarget, speed * Time.deltaTime);
            potion.transform.Rotate(0f, 0f, potion.transform.rotation.z);
            yield return null;
        }

        if (potion == null) yield break;
        potion.transform.position = finalTarget;
        Destroy(potion);
        potionTimer = potionTimerReset;
    }

    void ThrowBearTrap()
    {
        if (bearTrapPrefab == null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        GameObject trap = Instantiate(bearTrapPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * bearTrapThrowForce, ForceMode2D.Impulse);
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

