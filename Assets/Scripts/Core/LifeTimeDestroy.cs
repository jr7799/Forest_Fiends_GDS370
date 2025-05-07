using UnityEngine;

public class LifeTimeDestroy : MonoBehaviour
{
    public float time = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, time);
    }
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
