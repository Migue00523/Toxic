using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float chaseSpeed = 6f; // Un poco más rápido para que sea inevitable
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target == null) return;

        // Moverse siempre hacia la posición del otro jugador en el eje X
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        transform.position += new Vector3(direction * chaseSpeed * Time.deltaTime, 0, 0);

        // Voltear el sprite hacia donde está la presa
        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
