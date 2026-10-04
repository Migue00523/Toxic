using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeRemaining = 15f;
    [SerializeField] private GameManager gameManager;

    private bool timerRunning = true;

    private void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;

            TimeOut();
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeOut()
    {
        Debug.Log("¡Se acabó el tiempo!");

        if (gameManager != null)
        {
            gameManager.TimeOut();
        }
        else
        {
            Debug.LogWarning("Falta asignar el GameManager en el Inspector del Timer.");
        }
    }
}