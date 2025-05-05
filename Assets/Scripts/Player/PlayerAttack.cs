using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

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
    public float whipResetInterval = 12f;
    public float whipSpawnDecrement = 0.15f;

    [Header("ScreenShake")]
    public ScreenShake screenShake;
    public float magnitude = 0.2f;
    public float duration = 0.2f;

    private float nextFireTime = 0f;
    private Camera cam;

    private GameObject[] bibles;
    private float orbitTimer;
    private float bibleResetTimer;
    private float whipResetTimer;
    private bool isFading = false;
    private Coroutine fadeCoroutine;

    private enum AttackState { Bible, Whip }
    private AttackState currentState = AttackState.Bible;

    [Header("Whip Timer")]
    public float whipTimer;
    public float whipTimerReset;
    [Header("Bible Timer")]
    public float bibleTimer;
    public float bibleTimerReset;

    public bool whipActive = false;
    public bool bibleActive = false;

    SoundManager soundManager;

    void Start()
    {
        cam = Camera.main;
        screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        orbitTimer = bibleOrbitDuration;
        bibleResetTimer = bibleResetInterval;
        whipResetTimer = whipResetInterval;
    }

    void Update()
    {
        HandleShooting();

        if (currentState == AttackState.Bible)
        {
            HandleBibleLogic();
        }
        else if (currentState == AttackState.Whip)
        {
            if (whipActive)         
                HandleWhipLogic();
            else
            {
                currentState = AttackState.Bible;
            }
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
        if(bibleActive)
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
                bibleResetTimer -= Time.deltaTime * 2;

                if (bibleResetTimer <= 0f)
                {
                    whipResetTimer = whipResetInterval;
                    currentState = AttackState.Whip; // Switch to whip mode
                    
                }
            }
        }
    }

    void HandleWhipLogic()
    {
        whipResetTimer -= Time.deltaTime * 2;

        if (whipResetTimer <= 0f)
        {
            TriggerWhipEffect();
        
            // Switch back to Bible mode
            bibleResetInterval = Mathf.Max(1f, bibleResetInterval - bibleSpawnDecrement);
            whipResetInterval = Mathf.Max(1f, whipResetInterval - whipSpawnDecrement);
            ResetBibleOrbit();
            currentState = AttackState.Bible;
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
        bibleResetTimer = bibleResetInterval;
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
        bibleResetTimer = bibleResetInterval;
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
}

