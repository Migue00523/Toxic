using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Controls")]
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode jumpKey;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGround();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        float horizontalInput = 0f;
        if (Input.GetKey(leftKey)) horizontalInput = -1f;
        if (Input.GetKey(rightKey)) horizontalInput = 1f;

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput * moveSpeed));
        anim.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        float movement = 0f;

        if (Input.GetKey(leftKey))
        {
            movement = -1f;
        }

        if (Input.GetKey(rightKey))
        {
            movement = 1f;
        }

        rb.linearVelocity = new Vector2(movement * moveSpeed,rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpForce);

        anim.SetTrigger("Jump");
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }
}