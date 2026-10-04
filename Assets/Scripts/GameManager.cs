using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Transition")]
    [SerializeField] private Image blackScreenPanel; // Arrastra tu Panel negro aquí
    [SerializeField] private float fadeDuration = 2f;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private bool timeIsUp = false;

    private void Update()
    {
        // Ejemplo: Simula que el tiempo se acaba (reemplaza esto con tu variable real de tiempo)
        // if (tiempoRestante <= 0 && !timeIsUp)
        // {
        //     StartCoroutine(TriggerMutationEvent());
        // }
    }

    // Llama a este método cuando el tiempo llegue a cero
    public void TimeOut()
    {
        if (timeIsUp) return;
        timeIsUp = true;

        StartCoroutine(TriggerMutationEvent());
    }

    private IEnumerator TriggerMutationEvent()
    {
        // 1. Activar pantalla negra instantánea o hacer fundido
        if (blackScreenPanel != null)
        {
            blackScreenPanel.gameObject.SetActive(true);
            blackScreenPanel.color = new Color(0, 0, 0, 1); // Pantalla totalmente negra
        }

        // 2. Esperar los 2 segundos pedidos
        yield return new WaitForSeconds(2f);

        // 3. Mutar a uno de los jugadores (Por ejemplo, Player 2 se vuelve el monstruo)
        MutatePlayer(player2, player1);

        // 4. Quitar la pantalla negra para revelar el horror
        if (blackScreenPanel != null)
        {
            blackScreenPanel.gameObject.SetActive(false);
        }
    }

    private void MutatePlayer(GameObject mutatedPlayer, GameObject targetPlayer)
    {
        // Desactivar el script de control normal del jugador mutado
        PlayerController normalController = mutatedPlayer.GetComponent<PlayerController>();
        if (normalController != null)
        {
            normalController.enabled = false;
        }

        // Activar el Animator de muerte/mutación o cambiar su velocidad a una más rápida y aterradora
        Animator anim = mutatedPlayer.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Dead", false); // O un bool de "Mutated" si lo creas
            // Podrías activar una animación de monstruo aquí
        }

        // Añadir el script cazador (IA que persigue al targetPlayer sin piedad)
        MonsterAI hunterAI = mutatedPlayer.AddComponent<MonsterAI>();
        hunterAI.target = targetPlayer.transform;
    }
}