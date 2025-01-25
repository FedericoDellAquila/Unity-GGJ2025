using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour, IPlayerDamager
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.health.ApplyDamage(damage);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
