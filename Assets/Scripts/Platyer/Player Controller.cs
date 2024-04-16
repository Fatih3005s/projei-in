using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumpCount = 3;
    [SerializeField] private int jumpCount = 0;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            animator.SetInteger("JumpCount", jumpCount);
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        animator.SetInteger("JumpCount", jumpCount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            jumpCount = 0;
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }

        // Yeni eklenen kısım:
        if (collision.gameObject.CompareTag("NextScene")) // Eğer çarpıştığımız obje "NextScene" etiketine sahip ise
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Bir sonraki sahneyi yükle
        }
    }
}
