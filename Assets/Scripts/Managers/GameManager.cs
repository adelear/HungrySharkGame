using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager> 
{ 
    [SerializeField] GameObject HealthImg;
    [SerializeField] AudioManager asm; 
    [SerializeField] AudioClip LossSound;
    public enum GameState
    {
        GAME,
        PAUSE,
        DEFEAT,
        WIN
    }
    [SerializeField] private GameState currentState; 

    public event Action OnGameStateChanged;

    //Upon change in value in Score, subscribed methods will execute. 
    //Score will be updated in UI in CanvasManager
    public int Score
    {
        get => score;
        set
        {
            score = value;

            if (OnScoreValueChanged != null)
                OnScoreValueChanged.Invoke(score); 
        }
    }
    private int score = 0;
    public UnityEvent<int> OnScoreValueChanged;

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;

            if (lives > maxLives) lives = maxLives;

            Debug.Log("Lives value has changed to " + lives.ToString());
            if (lives < 0)
                StartCoroutine(DelayedGameOver(0.5f)); 

            DecreaseHealthBar(); 

            if (OnLifeValueChanged != null)
                OnLifeValueChanged.Invoke(lives);
        }
    }

    public UnityEvent<int> OnLifeValueChanged;

    private int lives = 3;
    public int maxLives = 3;

    public void DecreaseHealthBar()
    {
        if (HealthImg != null)
        {
            RectTransform healthRectTransform = HealthImg.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta -= new Vector2(10f, 0f); 
        }
    }

    IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOver();
    }

    void GameOver()
    {
        SwitchState(GameState.DEFEAT);
        SceneTransitionManager.Instance.LoadScene("MainMenu");
        asm.PlayOneShot(LossSound, false);  
    }

    public GameState GetGameState()
    {
        return currentState;
    }

    public void SwitchState(GameState newState)
    {
        Debug.Log("New state has been set to " + newState); 
        currentState = newState;
        OnGameStateChanged?.Invoke(); 
    }
}

