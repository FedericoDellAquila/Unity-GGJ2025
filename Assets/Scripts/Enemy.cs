using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Bubble.BubbleType type;
    private Health _health;

    public Enemy(Bubble.BubbleType type)
    {
        
    }
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.onDeath.AddListener(Annihilate);
    }

    private void Annihilate()
    {
        _health.onDeath.RemoveAllListeners();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        
        Bubble bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble && bubble.type == type)
        {
            _health.ApplyDamage(34);
        }
    }
}
