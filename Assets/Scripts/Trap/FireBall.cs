using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 5f; // Hız
    public bool moveRight = true; // Sağa mı gidiyor?

    void Update()
    {
        // Objeyi sağa veya sola doğru hareket ettir
        if (moveRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
