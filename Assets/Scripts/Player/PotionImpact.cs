using UnityEngine;
using UnityEngine.Events;

public class PotionImpact : MonoBehaviour
{
    public GameObject prefab;
    public GameObject pop;
    public Vector3 position;
    public UnityEvent destroyed;
    private void OnDestroy()
    {
        destroyed.Invoke();
        Instantiate(pop, position, Quaternion.identity);
        Instantiate(prefab, position, Quaternion.identity);
    }
}
