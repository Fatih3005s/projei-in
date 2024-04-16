using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public PlayerHealth playerHealth; // PlayerHealth scriptine erişmek için referans
    private Text healthText; // Canı göstermek için UI'daki metin nesnesine erişmek için referans
    private Text recoveryTimeText; // Recovery zamanını göstermek için UI'daki metin nesnesine erişmek için referans

    void Start()
    {
        // UI nesnelerine erişim alınır
        healthText = transform.Find("HealthText").GetComponent<Text>();
        recoveryTimeText = transform.Find("RecoveryTimeText").GetComponent<Text>();
    }

    void Update()
    {
        // Canı ve iyileşme zamanını UI'da gösterilmesini sağlar
        UpdateHealthUI();
        UpdateRecoveryTimeUI();
    }

    // Can değerini güncelleyen fonksiyon
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + playerHealth.Health.ToString();
    }

    // İyileşme zamanını güncelleyen fonksiyon
    void UpdateRecoveryTimeUI()
    {
        recoveryTimeText.text = "Recovery Time: " + playerHealth.RecoveryTime.ToString();
    }
}

