using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float pointMultiplier;
    [SerializeField] TextMeshProUGUI counterText;
    private float points;

    void Update()
    {
        points += Time.deltaTime * pointMultiplier;
        counterText.text = points.ToString("0");
    }
}
