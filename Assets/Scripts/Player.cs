using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Serializable]
    public struct WeaponBubbleTypeThreshold
    {
        public float minThreshold;
        public float maxThreshold;
        public Bubble.BubbleType type;
    }
    
    public Health health;
    private PlayerMovement _playerMovement;
    [SerializeField] private Bubbler _bubbler;
    [SerializeField] public List<WeaponBubbleTypeThreshold> weaponBubbleTypeThresholds;
    
    [SerializeField] private BoxCollider boxCollider;

    private Rigidbody _rigidbody;
    private MicrophoneLoudnessDetector _microphoneLoudnessDetector;

    private float loudness;
    
    private void Awake()
    {
        _microphoneLoudnessDetector = GetComponent<MicrophoneLoudnessDetector>();
        boxCollider = GetComponent<BoxCollider>();
        health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return;

        loudness = _microphoneLoudnessDetector.loudness;

        if (Input.GetMouseButtonDown(0))
        {
            _bubbler.SpawnBubble(DetermineBubbleType(loudness));
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        IPlayerDamager playerDamager = other.gameObject.GetComponent<IPlayerDamager>();
        if (playerDamager != null)
        {
            int damage = playerDamager.GetDamage();
            health.ApplyDamage(damage);
            Destroy(this.gameObject);
        }
    }

    private Bubble.BubbleType DetermineBubbleType(float value)
    {
        Bubble.BubbleType type = Bubble.BubbleType.Neutral;
        
        foreach (WeaponBubbleTypeThreshold typeThreshold in weaponBubbleTypeThresholds)
        {
            if (value >= typeThreshold.minThreshold && value <= typeThreshold.maxThreshold)
                type = typeThreshold.type;
        }
        return type;
    }
}
