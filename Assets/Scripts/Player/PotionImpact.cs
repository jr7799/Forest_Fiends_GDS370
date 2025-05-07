using UnityEngine;

public class PotionImpact : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //}

        
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
            
        //}

        Destroy(gameObject); 
    }
}
