using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FrequencyBand
{
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
            "\nRearRight")]
        private AudioChannel channel = AudioChannel.Left;
        [Range(8, 12)] public int Pow = 9;
        [SerializeField]  int FreqBandSize = 8;
        int FreqBandPower = 1;

        [Space(3)]
        [Header("[OutPut]")]
        public float[] Samples; // 512 samples
        public float[] FreqBand; 

        private void Awake()
        {
            CreateInstance();
            Samples = new float[(int)Mathf.Pow(2, Pow)];
            FreqBand = new float[FreqBandSize];
        }

        private void Update()
        {
            GetSpectrum();
            FrequencyBand();
        }

        void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void GetSpectrum()
        {
            audioSource.GetSpectrumData(samples: Samples, channel: (int)channel, FFTWindow.Blackman);
        }

        void FrequencyBand()
        {
            int cnt = 0;

            for(int i =0; i<8; i++)
            {
                float avg = 0;
                int sampleCnt = (int)Mathf.Pow(2,i)*2;

                if (i == 7)
                {
                    sampleCnt += 2;
                }

                for(int j = 0; j<sampleCnt; j++)
                {
                    avg += Samples[cnt];
                    cnt++;
                }

                avg /= sampleCnt;
                FreqBand[i] = avg*FreqBandPower;
            }
        }
    }
}