using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Transition")]
    [SerializeField] private Image blackScreenPanel;
    [SerializeField] private float fadeDuration = 2f;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Monster Spawn")]
    [SerializeField] private GameObject monsterPrefab; 

    [SerializeField] private AudioSource audioSource;   
    [SerializeField] private AudioClip growlSoundClip;

    private bool timeIsUp = false;

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

        if (audioSource != null && growlSoundClip != null)
        {
            audioSource.PlayOneShot(growlSoundClip);
        }

        
        yield return new WaitForSeconds(2f);

       
        if (player2 != null && monsterPrefab != null)
        {
            // Guardamos la posición exacta donde estaba el Player 2
            Vector3 spawnPosition = player2.transform.position;

            // Desactivamos al Player 2
            player2.SetActive(false);

            // Instanciamos el monstruo en esa misma posición
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // Le asignamos a la IA del monstruo que persiga al Player 1
            MonsterAI hunterAI = spawnedMonster.GetComponent<MonsterAI>();
            if (hunterAI != null && player1 != null)
            {
                hunterAI.target = player1.transform;
            }
        }

        if (blackScreenPanel != null)
        {
            blackScreenPanel.gameObject.SetActive(false);
        }
    }
}