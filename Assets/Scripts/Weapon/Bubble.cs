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
    
    private MeshRenderer _renderer;

    public GameObject target;

    private GameObject aimPlane;

    private float time;
    private Vector3 originalScale = new Vector3(0.2f, 0.2f, 1.0f);

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        aimPlane = GameObject.Find("AimPlane");

        originalScale.x /= 50;
        originalScale.y /= 50;
    }

    private void Start()
    {
        _renderer.material.color = _colorData.GetColor(type);
        
        StartCoroutine(DestroyTimer());
    }
    
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    [SerializeField] private float cap;
    
    private void Update()
    {
        time += Time.deltaTime;
        float normalized = Mathf.Clamp(time / cap, 0.0f, 1.0f);
        this.transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale, normalized);
        
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