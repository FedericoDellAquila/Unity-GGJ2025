using UnityEngine;

public class HeadbobSystem : MonoBehaviour
{
    
    [SerializeField] private float Amount = 1f;

    [SerializeField] private float Frequency = 10f;
    
    [Range(10f, 100f), SerializeField]
    private float Smooth = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Time.deltaTime * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x = Mathf.Lerp(pos.x, Mathf.Sin(Time.time * Frequency /2f) * Time.deltaTime * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;
    }
}
