using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("Movement")]
    public Transform target;
    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float catchDistance = 1f; // Distancia a la que se considera que atrapó al jugador

    [Header("References")]
    [HideInInspector] public GameManager gameManagerRef;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool hasCaughtPlayer = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target == null || hasCaughtPlayer) return;

        // 1. Calcular la distancia y dirección hacia el jugador
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        float direction = target.position.x - transform.position.x;
        float moveDirection = Mathf.Sign(direction);

        // 2. Comprobar si el monstruo atrapó al jugador
        if (distanceToTarget <= catchDistance)
        {
            CatchPlayer();
            return;
        }

        // 3. Mover al monstruo hacia la presa
        transform.position += new Vector3(moveDirection * chaseSpeed * Time.deltaTime, 0, 0);

        // 4. Activar animación de caminar
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

        // 5. Voltear el sprite hacia el jugador
        if (moveDirection > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void CatchPlayer()
    {
        hasCaughtPlayer = true;

        // Detener la animación de caminar del monstruo
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }

        // Activar la animación de muerte del jugador atrapado (Usando "Dead" como me indicaste)
        Animator playerAnim = target.GetComponent<Animator>();
        if (playerAnim != null)
        {
            playerAnim.SetTrigger("Dead"); // <-- Cambiado a "Dead"
        }

        // Desactivar el control del jugador para que se quede estático al morir
        PlayerController playerController = target.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Llamar al GameManager para mostrar el panel de Game Over tras un breve instante
        if (gameManagerRef != null)
        {
            gameManagerRef.Invoke("TriggerGameOver", 1f); // Espera 1 segundo para apreciar la animación
        }
    }
}