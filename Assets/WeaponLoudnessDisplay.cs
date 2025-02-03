using UnityEngine;

public class WeaponLoudnessDisplay : MonoBehaviour
{
    [SerializeField] MicrophoneLoudnessDetector loudnessDetector;
    private MeshRenderer _meshFilter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TryGetComponent<MeshRenderer>(out MeshRenderer meshFilterComponent))
        {
            _meshFilter = meshFilterComponent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = Mathf.Clamp(loudnessDetector.loudness,0 ,1);
        
        Color newColor = Color.black;
        
        if (loudness >= 0 && loudness < 0.25)
        {
            newColor = Color.white;
        } else if (loudness >= 0.25 && loudness < 0.5)
        {
            newColor = Color.red;
        } else if (loudness >= 0.5 && loudness < 0.75)
        {
            newColor = Color.green;
        } else if (loudness >= 0.75 && loudness <= 1)
        {
            newColor = Color.blue;
        }
            
        _meshFilter.material.SetColor("_Color", newColor);
    }
}
