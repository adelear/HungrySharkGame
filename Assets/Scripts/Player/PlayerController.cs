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

    private float sharkFollowSpeed = 5f;
    private float followDelay = 0.5f; 

  
    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
