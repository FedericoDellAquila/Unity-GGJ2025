using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    private int _counter;

    private void Awake()
    {
        _counter = hearts.Length - 1;
    }

    public void DamageReceived()
    {
        hearts[_counter].SetActive(false);
        _counter--;
    }
}
