using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    public static AudioPeer Instance { get; private set; } 
    
    
    public enum AudioChannel // 5.1 Channel
    {
        Left,
        Right,
        Center,
        Subwoofer,
        RearLeft,
        RearRight,
    }

    [Header("[Ref]")]
    [SerializeField] private AudioSource audioSource;

    [Space(3)]
    [Header("[Value]")]
    [SerializeField, Tooltip("Left : Stereo or Mono Channel" +
        "\nRight : Stereo" +
        "\nCenter " +
        "\nSubwoofer :  LFE(Low Frequency Effects)" +
        "\nRearLeft " +
        "\nRearRight")] private AudioChannel channel = AudioChannel.Left;
    [Range(8, 12)] public int Pow = 8;

    [Space(3)]
    [Header("[OutPut]")]
    public float[] Samples;

    private void Awake()
    {
        CreateInstance();
        Samples = new float[(int)Mathf.Pow(2, Pow)];
    }

    private void Update()
    {
         GetSpectrum();
    }

    void CreateInstance()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    void GetSpectrum()
    {
        audioSource.GetSpectrumData(samples:Samples, channel:(int)channel, FFTWindow.Blackman);
    }
}
