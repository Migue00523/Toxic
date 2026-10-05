using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string nextSceneName = "Scene2"; // Ya viene predeterminado

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            // Buscamos tu GameSceneManager en la escena
            GameSceneManager sceneManager = FindObjectOfType<GameSceneManager>();

            if (sceneManager != null)
            {
                Debug.Log("¡Nivel completado! Cargando: " + nextSceneName);
                sceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("No se encontró ningún GameSceneManager en la escena.");
            }
        }
    }
}