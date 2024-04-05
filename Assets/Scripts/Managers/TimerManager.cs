using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime = 300f; 
    private float currentTime;

    private void Start()
    {
        currentTime = startTime; 
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        if (currentTime <= 0)
        {
            GameManager.Instance.SwitchState(GameManager.GameState.WIN);
            SceneTransitionManager.Instance.LoadScene("MainMenu"); 
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
