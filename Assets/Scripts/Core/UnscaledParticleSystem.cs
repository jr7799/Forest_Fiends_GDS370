using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UnscaledParticleSystem : MonoBehaviour
{
    private ParticleSystem ps;
    private float lastUnscaledTime;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play(); // Start playing if not already
        lastUnscaledTime = Time.unscaledTime;
    }

    void Update()
    {
        float delta = Time.unscaledTime - lastUnscaledTime;
        ps.Simulate(delta, true, false); // Simulate with unscaled delta
        lastUnscaledTime = Time.unscaledTime;
    }
}
