using System;
using System.Collections.Generic;
using UnityEngine;

public class Bubbler : MonoBehaviour
{
    [SerializeField] private List<GameObject> bubbles = new List<GameObject>();
    
    public void SpawnBubble(Bubble.BubbleType type)
    {
        GameObject go = bubbles.Find((GameObject g) => g.GetComponent<Bubble>().type == type);
        GameObject bubble = Instantiate(go, this.transform.position, this.transform.rotation);

        Quaternion rotation = Quaternion.LookRotation(this.transform.forward, this.transform.up);
        bubble.transform.rotation = rotation;
    }
}
