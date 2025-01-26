using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "Finish")
        {
            GameObject otherParent = other.gameObject.transform.parent.gameObject;
            List<EnemySpawnPoint> spawnPoints = otherParent.GetComponentsInChildren<EnemySpawnPoint>().ToList();
            foreach (EnemySpawnPoint spawnPoint in spawnPoints)
            {
                if (spawnPoint.GetComponentInChildren<Enemy>())
                {
                    Debug.Log("lol");
                    Destroy(other.gameObject);
                    player.health.ApplyDamage(1);
                    break;
                }
            }
        }
    }
}
