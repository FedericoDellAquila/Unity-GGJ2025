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

    [SerializeField] private ColorData _colorData;
    public float speed = 10.0f;
    public BubbleType type;

    public LayerMask layerMask;
    
    private SpriteRenderer _renderer;

    public GameObject target;

    private GameObject aimPlane;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        aimPlane = GameObject.Find("AimPlane");
    }

    private void Start()
    {
        _renderer.color = _colorData.GetColor(type);
        
        StartCoroutine(DestroyTimer());
    }
    
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
    
    private void Update()
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance < 0.2f)
            Destroy(this.gameObject);
        
        if ( aimPlane.transform.position.z < this.transform.position.z)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())
            return;
        
        if (other.gameObject.layer == layerMask)
            Destroy(this.gameObject);

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Destroy(target);
    }
}