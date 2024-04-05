using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     A Hungry Shark that can only eat fish that is smaller than him 
    Following mouse cursor to move with a delay 
    Upon eating fish it can’t eat, it gets sick and gets a temporary effect (Slower?)
    It grows bigger with each smaller fish it can eat
    Lose a life when you try to eat a fish bigger than you
    Speed fishes that increase your speed when you consume them
    */

    SpriteRenderer sr; 
    private float sharkFollowSpeed = 1f;
    private float followDelay = 0.5f;
    private Vector2 targetPosition; 

    private void Start()
    {
        targetPosition = transform.position;
        sr = GetComponentInChildren<SpriteRenderer>(); 
    }

    private void Update()
    {
        HandleMovement(); 
        RotatePlayerTowardsCursor(); 
    }

    // Probably best to handle collision on external classes or make a PlayerCollision Script
    private void OnCollisionEnter2D(Collision2D collision)
    {
        NPCFish fish = collision.gameObject.GetComponent<NPCFish>();
        if (fish == null) return; 
    }

    private void EatFish(EdibleFish fish)
    {
        // Increasing size shark
        transform.localScale += Vector3.one * 0.1f;
        Destroy(fish.gameObject);
    }

    private void LoseLife()
    {
        // Decreasingg size of shark
        transform.localScale -= Vector3.one * 0.1f;
    }

    private void HandleMovement()
    {
        if (followDelay <= 0f) targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else followDelay -= Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, targetPosition, sharkFollowSpeed * Time.deltaTime);
    }

    private void RotatePlayerTowardsCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
