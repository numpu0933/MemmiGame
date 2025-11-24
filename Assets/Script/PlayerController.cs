using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        movement = Vector2.zero;

        // กระโดด
        if ((keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // เดินซ้าย-ขวา
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) movement.x = -1;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) movement.x = 1;

        // flip sprite
        if (movement.x > 0) spriteRenderer.flipX = false;
        else if (movement.x < 0) spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        // ตรวจสอบพื้น
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // เคลื่อนที่แนวนอน
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
