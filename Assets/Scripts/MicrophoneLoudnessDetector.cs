using System;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneLoudnessDetector : MonoBehaviour
{
    public string microphoneDevice; // The microphone device to use
    public int sampleWindow = 128; // Number of samples to analyze for loudness

    [Range(0.0f, 1.0f)]
    public float loudness = 0.0f;
    public float maxLoudness = 0.5f;
    public float multiplier = 3;
    public float threshold = 0.25f;

    public int bufferSize = 8;

    public Queue<float> buffer = new Queue<float>();
    public float average;
    
    private AudioClip microphoneClip;

    private void Awake()
    {
        buffer = new Queue<float>(bufferSize);
    }

    void Start()
    {
        // List all available microphones
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Microphone detected: " + device);
        }

        // Use the first microphone found if not specified
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0];
            StartMicrophone();
        }
        else
        {
            Debug.LogError("No microphone devices found!");
        }
    }

    void StartMicrophone()
    {
        microphoneClip = Microphone.Start(microphoneDevice, true, 20, AudioSettings.outputSampleRate);
        if (microphoneClip == null)
        {
            Debug.LogError("Failed to start microphone!");
        }
    }

    void Update()
    {
        if (microphoneClip != null)
        {
            if (buffer.Count > bufferSize)
                buffer.Dequeue();
                
            buffer.Enqueue(GetLoudnessFromMicrophone());

            foreach (float val in buffer)
                average += val;
            
            average /= buffer.Count;

            loudness = Mathf.Clamp((average / maxLoudness) * multiplier, 0.0f, 1.0f);
        }
    }

    float GetLoudnessFromMicrophone()
    {
        // Create a buffer to hold audio samples
        float[] audioSamples = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(microphoneDevice) - sampleWindow;

        // Handle buffer wrap-around
        if (microphonePosition < 0) return 0;

        // Copy samples from the microphone clip
        microphoneClip.GetData(audioSamples, microphonePosition);

        // Calculate the RMS value of the samples
        //float sum = 0f;
        //for (int i = 0; i < sampleWindow; i++)
        //{
        //    sum += audioSamples[i] * audioSamples[i];
        //}
        //return Mathf.Sqrt(sum / 
        
        float sum = 0f;
        foreach (float sample in audioSamples)
        {
            sum += Mathf.Abs(sample); // Take the absolute value for linear scaling
        }

        float currentLoudness = sum / audioSamples.Length; // Average of absolute values
        return currentLoudness;
    }

    void OnDestroy()
    {
        if (Microphone.IsRecording(microphoneDevice))
        {
            Microphone.End(microphoneDevice);
        }
    }
}
