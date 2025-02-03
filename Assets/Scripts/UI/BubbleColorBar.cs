using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleColorBar : MonoBehaviour
{
    [SerializeField] private MicrophoneLoudnessDetector microphoneLoudnessDetector;

    private List<Slider> quartersSliders = new List<Slider>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Slider>(out Slider sliderComponent))
            {
                quartersSliders.Add(sliderComponent);
            }
        }
    }

    void Update()
    {
        if (microphoneLoudnessDetector == null)
        {
            // Debug.LogError("MicrophoneLoudnessDetector is null");
            return;
        }
        float loudnessValue = microphoneLoudnessDetector.loudness;

        if (loudnessValue > 1f)
        {
            loudnessValue = 1f;
        }

        loudnessValue *= 4f;
        foreach (Slider slider in quartersSliders)
        {
            if (loudnessValue > 1f)
            {
                slider.value = 1f;
                loudnessValue -= 1f;
                continue;
            }
            slider.value = loudnessValue;
            loudnessValue = 0f;
        }
    }
}