using UnityEngine;

public class PotionImpact : MonoBehaviour
{
    public GameObject prefab;
    public GameObject pop;
    public Vector3 position;
    private void OnDestroy()
    {
        Instantiate(pop, position, Quaternion.identity);
        Instantiate(prefab, position, Quaternion.identity);
    }
}
