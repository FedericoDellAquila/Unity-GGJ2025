using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] public ColorData ColorData;
    [SerializeField] private GameObject enemyPrefabs;
    
    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, (int)Bubble.BubbleType.Blue + 1);

        Bubble.BubbleType type = (Bubble.BubbleType)randomIndex;
        
        GameObject go = Instantiate(enemyPrefabs, transform.position, Quaternion.identity, this.transform);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.type = type;
        
        enemy.GetComponentInChildren<MeshRenderer>().material.color = ColorData.GetColor(type);
    }
}
