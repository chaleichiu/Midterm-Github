using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class UITimer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;

    [Header("Timer Settings")]
    [SerializeField] private float duration = 30f;
    [SerializeField] private UnityEvent _event;


    private float timeRemaining;
    private bool isTimerRunning = false;

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
        UpdateTimerDisplay(duration);
    }

    private void Update()
    {
        if (!isTimerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            isTimerRunning = false;
            UpdateTimerDisplay(0);
            TimerFinished();
        }
    }

    public void StartTimer()
    {
        if (isTimerRunning) return;
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
        timeRemaining = duration;
        isTimerRunning = true;
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;

        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
    }

    private void TimerFinished()
    {
        _event.Invoke();
        Debug.Log("Timer ended!");
    }
}
