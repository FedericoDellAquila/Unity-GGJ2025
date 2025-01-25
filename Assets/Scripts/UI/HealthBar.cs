using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        playerHealth.onDamageReceived.AddListener(DamageReceivedCallback);
        playerHealth.onHealthRecovered.AddListener(HealthRecoveredCallback);
    }

    private void HealthRecoveredCallback()
    {
        healthBar.value = playerHealth.NormalizedHealth();
    }

    private void DamageReceivedCallback()
    {
        healthBar.value = playerHealth.NormalizedHealth();
    }
}
