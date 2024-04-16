using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Hareket hızı
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumpCount = 3; // Maksimum zıplama sayısı
    [SerializeField] private int jumpCount = 0;
    
    private bool isGrounded; // Yerde mi değil mi?
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
            animator.SetInteger("JumpCount", jumpCount);
        }

        // -Y yönünde yapılan hareket kontrolü ve isFalling kontrolü
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
        if (collision.gameObject.CompareTag("Ground")) // Eğer çarpıştığımız obje "Ground" etiketine sahip ise
        {
            isGrounded = true; // Yere değdiğimizi belirtmek için isGrounded'i true yap
            animator.SetBool("isGrounded", true); // Animator'a da yere değdiğimizi iletiyoruz
            jumpCount = 0; // Yerdeyken zıplama sayısını sıfırla
        }
        else 
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    
    }


}
