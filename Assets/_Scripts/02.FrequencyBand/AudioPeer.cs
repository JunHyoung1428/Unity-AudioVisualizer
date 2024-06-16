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
        [Range(8, 12)] public int pow = 9;
        [SerializeField]  int freqBandSize = 8;
        int FreqBandPower = 1;
        [SerializeField]  float decreaseRate = 0.005f;
        [SerializeField]  float IncreaseRate = 1.2f;

        [Space(3)]
        [Header("[OutPut]")]
        public float[] samples; // 512 samples
        public float[] freqBand;
        public float[] bandBuffer;
        float[] bufferDecrease;


        private void Awake()
        {
            CreateInstance();
            samples = new float[(int)Mathf.Pow(2, pow)];
            freqBand = new float[freqBandSize];
            bandBuffer = new float[freqBandSize];
            bufferDecrease = new float[freqBandSize];
        }

        private void Update()
        {
            GetSpectrum();
            FrequencyBand();
            BandBuffer();
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
            audioSource.GetSpectrumData(samples: samples, channel: (int)channel, FFTWindow.Blackman);
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
                    avg += samples[cnt];
                    cnt++;
                }

                avg /= sampleCnt;
                freqBand[i] = avg*FreqBandPower;
            }
        }

        void BandBuffer()
        {
            for (int i = 0; i < freqBandSize; ++i){
                if (freqBand[i] > bandBuffer[i])
                {
                    bandBuffer[i] = freqBand[i];
                    bufferDecrease[i] = decreaseRate;
                }

                if (freqBand[i] < bandBuffer[i])
                {
                    bandBuffer[i] -= bufferDecrease[i];
                    bufferDecrease[i] *= IncreaseRate;
                }
            }

        }
    }
}