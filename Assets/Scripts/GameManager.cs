using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Transition & Game Over")]
    [SerializeField] private Image blackScreenPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float fadeDuration = 2f;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Monster Spawn")]
    [SerializeField] private GameObject monsterPrefab;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip growlSoundClip;

    [Header("Scene Manager Reference")]
    [SerializeField] private GameSceneManager gameSceneManager; // <--- ¡AQUÍ ESTABA FALTANDO LA DECLARACIÓN!

    private bool timeIsUp = false;

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TimeOut()
    {
        if (timeIsUp) return;
        timeIsUp = true;

        StartCoroutine(TriggerMutationEvent());
    }

    private IEnumerator TriggerMutationEvent()
    {
        // 1. Activar pantalla negra
        if (blackScreenPanel != null)
        {
            blackScreenPanel.gameObject.SetActive(true);
            blackScreenPanel.color = new Color(0, 0, 0, 1);
        }

        // 2. Reproducir gruñido
        if (audioSource != null && growlSoundClip != null)
        {
            audioSource.PlayOneShot(growlSoundClip);
        }

        // 3. Esperar los 2 segundos
        yield return new WaitForSeconds(2f);

        // 4. Reemplazar al Jugador 2 por el Monstruo
        if (player2 != null && monsterPrefab != null)
        {
            Vector3 spawnPosition = player2.transform.position;
            player2.SetActive(false);

            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            MonsterAI hunterAI = spawnedMonster.GetComponent<MonsterAI>();
            if (hunterAI != null && player1 != null)
            {
                hunterAI.target = player1.transform;

                // <--- ¡IMPORTANTE! Pasamos esta referencia para que el monstruo pueda activar el Game Over
                hunterAI.gameManagerRef = this;
            }
        }

        // 5. Quitar la pantalla negra
        if (blackScreenPanel != null)
        {
            blackScreenPanel.gameObject.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameSceneManager != null)
        {
            gameSceneManager.PauseGame();
        }
    }
}