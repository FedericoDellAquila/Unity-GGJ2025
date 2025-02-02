using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public int currentHealth = 5;
    public int maxHealth = 5;
    public bool isDead;
    
    public UnityEvent onDamageReceived;
    public UnityEvent onHealthRecovered;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public float NormalizedHealth()
    {
        return ((float)currentHealth / (float)maxHealth);
    }

    public void ApplyDamage(int damage)
    {
        if (isDead) 
            return;
        
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            isDead = true;
            onDeath.Invoke();
        }
        
        onDamageReceived.Invoke();
    }
    
    public void RecoverHealth(int amount)
    {
        if (isDead) 
            return;
        
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            return;
        
        onHealthRecovered.Invoke();
    }
}
