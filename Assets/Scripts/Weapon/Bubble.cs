using System;
using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public enum BubbleType
    {
        Neutral,
        Red,
        Green,
        Blue
    }

    public float speed = 10.0f;
    public BubbleType type;

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }
    
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
    
    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())
            return;

        Destroy(this.gameObject);
    }
}