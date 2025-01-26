using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private int counter;

    private void Awake()
    {
        counter = hearts.Length;
        playerHealth.onDamageReceived.AddListener(DamageReceivedCallback);
    }

    private void DamageReceivedCallback()
    {
        hearts[counter - 1].SetActive(false);
        counter--;
    }
}
