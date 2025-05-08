using UnityEngine;

public class PotionImpact : MonoBehaviour
{
    public GameObject prefab;
    private void OnDestroy()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
