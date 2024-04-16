using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHp;
    [SerializeField] private int recovery;
    [SerializeField] private int recoveryTime = 90;
    private float lastDamageTime;
    private bool isGameOver = false;

    void Start()
    {
        maxHp = 100; // Maksimum can 100 olarak ayarlanıyor.
        health = maxHp; // Başlangıçta can 100 olarak ayarlanıyor.
        recovery = 10; // Her iyileşmede can 10 artacak.
        lastDamageTime = Time.time; // Son hasar zamanı başlangıçta şu anki zamana ayarlanıyor.
    }

    void Update()
    {
        // Eğer oyun devam ediyorsa iyileşme kontrolü yapılır.
        if (!isGameOver)
        {
            // Eğer recoveryTime sıfırdan büyükse ve son hasar zamanından recoveryTime kadar zaman geçtiyse, iyileşme işlemi gerçekleşir.
            if (Time.time - lastDamageTime >= recoveryTime)
            {
                Heal(recovery);
                lastDamageTime = Time.time; // Son hasar zamanı güncellenir.
            }
        }
    }

    // Hasar alındığında bu fonksiyon çağrılır.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0; // Can 0'dan küçük veya eşitse oyun durdurulur.
            GameOver();
        }
        lastDamageTime = Time.time; // Son hasar zamanı güncellenir.
    }

    // Oyunun durdurulması için bir fonksiyon.
    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Oyun zamanı durduruluyor.
        Debug.Log("Game Over!"); // Opsiyonel: Konsola oyunun durduğunu yazdırabiliriz.
        // Buraya oyunun durdurulmasıyla ilgili başka işlemler de eklenebilir.
    }

    // Canı belirli bir miktar iyileştiren fonksiyon.
    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHp)
        {
            health = maxHp; // Maksimum canı aşamaz, maksimumdan büyükse maksimuma ayarlanır.
        }
    }
}
