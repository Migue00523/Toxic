using UnityEngine;

public class AutoJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Transform nextFloorPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
            return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (nextFloorPosition != null)
        {
            player.transform.position = nextFloorPosition.position;
        }
    }
}