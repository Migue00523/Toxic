using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameSceneManager sceneManager;

    public void Pause()
    {
        pausePanel.SetActive(true);
        sceneManager.PauseGame();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        sceneManager.ResumeGame();
    }

    public void Restart()
    {
        sceneManager.RestartLevel();
    }
} 