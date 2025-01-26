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
            Slider slider = child.GetComponent<Slider>();
            if (slider != null)
            {
                quartersSliders.Add(slider);
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
        float value = microphoneLoudnessDetector.loudness;

        if (value > 1f)
        {
            value = 1f;
        }

        value *= 4f;
        foreach (Slider slider in quartersSliders)
        {
            if (value > 1f)
            {
                slider.value = 1f;
                value -= 1f;
                continue;
            }
            slider.value = value;
            value = 0f;
        }
    }
}