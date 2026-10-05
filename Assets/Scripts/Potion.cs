using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private bool isTimePotion;
    [SerializeField] private GameTimer gameTimer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo tocó la poción: " + other.gameObject.name);

        Transform player = other.transform.root;

        if (player.name != "Player1" && player.name != "Player2")
            return;

        Debug.Log("¡" + player.name + " recogió la poción!");

        if (isTimePotion)
        {
            if (gameTimer != null)
            {
                gameTimer.AddTime(10f);
                Debug.Log("¡Se agregaron 10 segundos!");
            }
            else
            {
                Debug.LogWarning("Falta asignar el GameTimer en la TimePotion.");
            }
        }

        Destroy(gameObject);
    }
}