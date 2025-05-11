using UnityEngine;
using UnityEngine.Events;

public class PotionImpact : MonoBehaviour
{
    public GameObject prefab;
    public GameObject pop;
    public Vector3 position;
    public UnityEvent destroyed;
    public PlayerAttack pAttack;
    private void Start()
    {
        pAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }
    private void OnDestroy()
    {
        destroyed.Invoke();
        pAttack.activePotions.Remove(gameObject);
        Instantiate(pop, position, Quaternion.identity);
        Instantiate(prefab, position, Quaternion.identity);
    }
}
