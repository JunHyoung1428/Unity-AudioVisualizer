using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AudioResponseUI
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

        public enum AudioFrequencySubsets
        {
            SubBass = 0, //16 to 60 Hz 
            Bass = 1, //60 to 250 Hz
            LowMidrange =2, //250 to 500 Hz
            MidRange = 3, //500 Hz to 2 kHz
            HighMidrange = 4, //2 to 4 kHz
            Presence = 5, //4 to 6 kHz	
            Brilliance = 6, //6 to 20 kHz
            Highs = 7,
        }

        /* # Note
         In this Script, 22050/512 = 43 herts per Sample
         # 0 - 2 = 86Hz
         # 1 - 4 = 172Hz (87 ~ 258)
         # 2 - 8 = 344Hz (259 ~ 602)
         # 3 - 16 = 688Hz (603 ~ 1290)
         # 4 - 32 = 1376Hz (1291 ~ 2666)
         # 5 - 64 = 2752Hz (2667 ~ 5418)
         # 6 - 128 = 5504Hz (5419 ~ 10922)
         # 7 - 256 = 11008Hz (109223 ~ 21930)
         # 512
         */

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