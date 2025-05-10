using System;
using System.Collections;
using UnityEngine;

public class MaxMagnet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var magnet = other.gameObject.GetComponent<Magnet>();
            StartCoroutine(MaxMagnetPull(magnet));
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private IEnumerator MaxMagnetPull(Magnet magnet)
    {
        var tempRadius = magnet.magnetRadius;
        var tempSpeed = magnet.pullSpeed;
        magnet.magnetRadius = tempRadius * 200;
        magnet.pullSpeed = 15;
        yield return new WaitForSeconds(5);
        magnet.pullSpeed = tempSpeed;
        magnet.magnetRadius = tempRadius;
        Destroy(gameObject);
    }
}
