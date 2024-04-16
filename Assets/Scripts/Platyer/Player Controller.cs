using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumpCount = 3;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int health;
    [SerializeField] private int maxHp;
    [SerializeField] private int damage;


    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHp;
    }

    private void Update()
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
        if (collision.gameObject.CompareTag("NextScene")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }

        if(collision.gameObject.CompareTag("TakeDamege"))
        {
            health -= damage;
            if (health <= 0)
            {
                Time.timeScale = 0f;
            }
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
