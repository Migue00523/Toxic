using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameSceneManager sceneManager;

    public void PlayGame()
    {
        sceneManager.LoadScene("Scene1");
    }

    public void ExitGame()
    {
        sceneManager.QuitGame();
    }
}
