using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] public float countdownMinutes = 5f;
    private float startTime;
    private bool isRunning = false;
    private float countdownDuration; 
    private float elapsedTime;

    void OnEnable()
    {
        StartTimer();
    }

    void Start()
    {
        // Convert CountdownMinutes to seconds
        countdownDuration = countdownMinutes * 60f;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            // Update the timer
            elapsedTime = countdownDuration - (Time.time - startTime);
            UpdateTimerDisplay();

            // Check if the countdown has reached zero
            if (elapsedTime <= 0f)
            {
                StopTimerAndSaveTime();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        // Format timer
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");
        timerText.text = "Time: " + minutes + ":" + seconds;
    }

    public void StartTimer()
    {
        isRunning = true;
        startTime = Time.time;
    }

    public void StopTimerAndSaveTime()
    {
        isRunning = false;
        PlayerPrefs.SetFloat("LevelTime", countdownDuration - elapsedTime);
    }

    public void PauseTimer()
    {
        if (isRunning)
        {
            // Pause the timer
            isRunning = false;
            elapsedTime = countdownDuration - (Time.time - startTime);
            UpdateTimerDisplay();
        }
    }

    public void ResumeTimer()
    {
        if (!isRunning)
        {
            // Resume the timer
            isRunning = true;
            startTime = Time.time - (countdownDuration - elapsedTime);
        }
    }
}
