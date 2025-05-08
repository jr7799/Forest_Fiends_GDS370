using UnityEngine;

public class PotionImpact : MonoBehaviour
{

    private void OnDestroy()
    {
        //Put instantiate here which will then spawn a new prefab or particle effect that will be the splash
        //the splash prefab needs a script thatll damage enemies on trigger enter
        //and trigger unless prefab then do the trigger like you did for the whip
        //have it destroy after like 5 seconds <-- this will be the variable that will be a rewards
        //so it lasts longer as well as doing more damage and larger scale or size so that it covers more ground
    }

    //dont think you need this for the star but will need for the new prefab that spawns when this is destroy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (explosionEffect != null)
        //{
        //    Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //}

        
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
            
        //}

        //Destroy(gameObject); 
    }
}
