using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Bubble.BubbleType type;
    private Health _health;

    private Rigidbody _rigidbody;

    private Camera playerCamera;
    
   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _health.onDeath.AddListener(Annihilate);
        playerCamera = Camera.main;
    }

    private void Update()
    {
    }

    private void Annihilate()
    {
        _health.onDeath.RemoveAllListeners();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bubble bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            if (bubble.type == type)
            {
                _rigidbody.useGravity = true;
                StartCoroutine(DestroyTimer());
            }
            
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Bubble bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            if (bubble.type == type)
            {
                _rigidbody.useGravity = true;
                StartCoroutine(DestroyTimer());
            }
            
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
