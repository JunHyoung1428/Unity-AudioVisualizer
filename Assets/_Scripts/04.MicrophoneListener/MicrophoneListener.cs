using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] float minScale = 1.0f;

    [SerializeField] AudioSource audioSource;    

    [Header("Micropphone  Setting")]
    [SerializeField] float sensitivity = 100.0f;
    [SerializeField] float soundScale = 1.0f;


    const int LENGTHSEC = 10;
    const int FRECUENCY = 44100; //44100 Hz or 48000 Hz

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(null, true, LENGTHSEC, FRECUENCY);
        audioSource.loop = true;


        while (!(Microphone.GetPosition(null) > 0)) { }

        audioSource.Play();

    }

    void Update()
    {
        UpdateScale();
    }

    void UpdateScale()
    {
        float loudness = GetAveragedVolume() * sensitivity;
        Vector3 newScale = new Vector3(loudness * soundScale + minScale, loudness * soundScale+ minScale, loudness * soundScale + minScale);
        targetObject.transform.localScale = newScale;
        Debug.Log(loudness);
    }


    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

}
