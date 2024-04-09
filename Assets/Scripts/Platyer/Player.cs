using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı
    public float jumpForce = 10f;
    public int maxJumpCount = 2; // Maksimum zıplama sayısı
    public bool isGrounded; // Yerde mi değil mi?

    private int jumpCount = 0; //Yapılan zıplama sayısı
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
        // Yatay hareket kontrolü
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Sprite'ın dönüşü
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // Eğer sola hareket ediyorsa sprite'ı ters çevir
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Eğer sağa hareket ediyorsa sprite'ı normal çevir
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        else
        {
            // Karakter hareket etmiyorsa, speed parametresine 0 atanarak durma animasyonuna geçiş sağlanır
            animator.SetFloat("Speed", 0);
        }

        // Zıplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        // -Y yönünde yapılan hareket kontrolü
        if (rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        // Animator'a zıplama sayısını iletiyoruz
        animator.SetInteger("JumpCount", jumpCount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Yere temas kontrolü
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;
            jumpCount = 0; // Yerdeyken zıplama sayısını sıfırla
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Yere temasın bitişi kontrolü
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = false;
        }
    }
}
