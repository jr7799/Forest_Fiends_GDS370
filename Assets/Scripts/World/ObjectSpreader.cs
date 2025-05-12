using UnityEngine;
using System.Collections.Generic;

public class ObjectSpreader : MonoBehaviour
{
    public List<GameObject> objectsToSpread; // Assign in Inspector or populate at runtime
    public float spreadRadius = 3f;

    void Start()
    {
        SpreadObjectsRandomly(objectsToSpread, spreadRadius);
    }

    public void SpreadObjectsRandomly(List<GameObject> objects, float radius)
    {
        Vector3 center = transform.position;

        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;

            // Generate a random point in the circle
            Vector2 randomOffset = Random.insideUnitCircle * radius;
            Vector3 newPos = new Vector3(center.x + randomOffset.x, center.y + randomOffset.y, center.z);

            obj.transform.position = newPos;
        }
    }
}
