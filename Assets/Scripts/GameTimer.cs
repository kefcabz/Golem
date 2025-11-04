using UnityEngine;
using TMPro; 
 
public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI gameTimerText; 

    void Update()
    {
        if (gameTimerText != null)
        {
            gameTimerText.text = "Time Protected: " + Time.time.ToString("F2"); // decimal places
        }
    }
}