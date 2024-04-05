using System;
using UnityEngine;

public class NPCFish : MonoBehaviour
{
    private float minX = -32f;
    private float maxX = 40f;
    private float minY = -32f;
    private float maxY = 31f;
    private float moveSpeed = 3f; 

    protected Vector3 targetPosition;
    protected Transform playerTransform;
    private float distanceThreshold = 5f; 

    public FishStates fishState;
    public FishStates FishState
    {
        get { return fishState; }
        set
        {
            if (fishState != value)
            {
                fishState = value;
                OnFishStateChanged?.Invoke(fishState);
                HandleStateChange(fishState);
                Debug.Log("State Changed to " +  fishState); 
            }
        }
    }
    public event Action<FishStates> OnFishStateChanged;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        fishState = FishStates.Roaming;
        SetRandomTargetPosition();
    }

    private void Update()
    {
        HandleStates();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime; 
    }

    private void SetRandomTargetPosition()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }
    
    //Only chases player when they are bigger than them (sickly and speedy fish will never be bigger than them) 
    // In the case that it does end up chasing the player, it changes state back to roaming
    private void ChasePlayer()
    {
        if (transform.localScale.magnitude <= playerTransform.localScale.magnitude)
        {
            fishState = FishStates.Roaming; 
            return;
        }
        targetPosition = playerTransform.position;
    }

    private void Eaten()
    {
        Destroy(gameObject); 
    }

    private bool PlayerWithinDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= distanceThreshold;
    }

    private void HandleStates()
    {
        if (PlayerWithinDistance())
        {
            fishState = FishStates.ChasePlayer;
        }
        else
        {
            fishState = FishStates.Roaming;
        }

        if (fishState == FishStates.Roaming)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) SetRandomTargetPosition();
        }
        else if (fishState == FishStates.ChasePlayer)
        {
            ChasePlayer();
        }
    }

    private void HandleStateChange(FishStates newState)
    {
        switch (newState)
        {
            case FishStates.Roaming:
                SetRandomTargetPosition();
                break;
            case FishStates.ChasePlayer:
                ChasePlayer(); 
                break;
            default:
                break;
        }
    }
}
