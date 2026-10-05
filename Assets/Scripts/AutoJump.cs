using UnityEngine;

public class AutoJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 15f;

    [Header("Destination")]
    [SerializeField] private Transform targetFloorPosition; // La posición a la que se teletransportará
    [SerializeField] private bool isGoingUp = true;         // Marca true si sube, false si baja

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
            return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Si va hacia arriba le da impulso, si va hacia abajo respeta la gravedad
            float appliedForce = isGoingUp ? jumpForce : 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, appliedForce);
        }

        if (targetFloorPosition != null)
        {
            // Teletransporte automático al tocar el collider
            player.transform.position = targetFloorPosition.position;
        }
    }
}