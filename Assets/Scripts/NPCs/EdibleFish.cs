using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleFish : NPCFish
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Detected collsion");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerTransform.localScale.magnitude >= transform.localScale.magnitude)
            {
                playerController.EatFish();
                FishSpawner.Instance.RemoveFishFromPool(gameObject); 
                Destroy(gameObject);
            }
            else
            {
                playerController.LoseLife();
                FishSpawner.Instance.RemoveFishFromPool(gameObject); 
                Destroy(gameObject);
            }
        }
    }
}
