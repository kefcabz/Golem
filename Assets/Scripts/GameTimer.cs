using UnityEngine;
using TMPro; 
 
public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI gameTimerText; 

    void Update()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Time.time.ToString("F2"); // decimal places
        }
    }
}