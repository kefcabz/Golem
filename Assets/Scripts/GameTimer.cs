using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI gameTimerText;

    private float elapsedTime = 0f;
    private bool timerRunning = false;

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;

            if (gameTimerText != null)
                gameTimerText.text = "Time Protected: " + elapsedTime.ToString("F2") + "s";
        }
    }
    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        if (gameTimerText != null)
            gameTimerText.text = "Time Protected: 0.00s";
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
