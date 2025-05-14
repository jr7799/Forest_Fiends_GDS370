using System.Collections;
using UnityEditor;
using UnityEngine;

public class PotionSplah : MonoBehaviour
{
    public float destroyTime = 3.5f;
    ParticleSystem splah;
    Color startColor;
    private void Start()
    {
        splah = GetComponent<ParticleSystem>();
        var mainModule = splah.main;
        startColor = mainModule.startColor.color;
        Color temp = new Color(startColor.r, startColor.g, startColor.b, 0);
        mainModule.startColor = new ParticleSystem.MinMaxGradient(temp);
        StartCoroutine(fadeIn());
    }
    public float damage = 5;
    [SerializeField] bool isCriticalHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCriticalHit = Random.Range(0, 100) < 30;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                if (!isCriticalHit)
                    enemy.TakeDamage(damage, false);
                else
                    enemy.TakeDamage(damage * 1.35f, true);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isCriticalHit = Random.Range(0, 100) < 30;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                if (!isCriticalHit)
                    enemy.TakeDamage(damage, false);
                else
                    enemy.TakeDamage(damage * 1.35f, true);
            }
        }
    }
    bool inside;
    float timer = .2f;
    float timerReset = 0.2f;
    private void Update()
    {
        if (inside)
        {
            timer -= Time.deltaTime;
        }
        
    }

    float lerpDuration = 1f;
    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(destroyTime);
        var mainModule = splah.main;
        float elapsedTime = 0f;
        Color off = new Color(startColor.r, startColor.g, startColor.b, 0);
        Color on = new Color(startColor.r, startColor.g, startColor.b, 1);
        Color lerpedColor;
        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;
            lerpedColor = Color.Lerp(on, off, t);
            mainModule.startColor = new ParticleSystem.MinMaxGradient(lerpedColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainModule.startColor = new ParticleSystem.MinMaxGradient(off);
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator fadeIn()
    {
        var mainModule = splah.main;
        float elapsedTime = 0f;
        Color off = new Color(startColor.r, startColor.g, startColor.b, 0);
        Color on = new Color(startColor.r, startColor.g, startColor.b, 1);
        Color lerpedColor;
        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;
            lerpedColor = Color.Lerp(off, on, t);
            mainModule.startColor = new ParticleSystem.MinMaxGradient(lerpedColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainModule.startColor = new ParticleSystem.MinMaxGradient(on);
        StartCoroutine(fadeOut());        
        yield return null;
    }
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
