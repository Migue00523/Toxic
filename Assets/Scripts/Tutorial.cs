using UnityEngine;

public class Tutorial : MonoBehaviour
{
   
    [SerializeField] private GameObject tutorialPanel; 

    private void Awake()
    {
        
        Time.timeScale = 0f;

        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
    }

    
    public void StartGame()
    {
        
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }
}