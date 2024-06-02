using UnityEngine;
using static AudioResponseUI.AudioPeer;



namespace AudioResponseUI
{
    public class AudioResponse : MonoBehaviour
    {
        [SerializeField] RectTransform rect;

        enum Mode
        {
            Turf,
            Lerp,
            None
        }
        [SerializeField] Mode mode;
        [Range(0f, 10f)]
        [SerializeField] float SizePower;
        [Range(0f, 0.1f)]
        [SerializeField] float smoothTime = 0.1f;

        [Space(5)]
        [Header("Minimum Scale")]
        [Range(0f, 2f)]
        [SerializeField] float MinXScale = 1f;
        [Range(0f, 2f)]
        [SerializeField] float MinYScale = 1f;
        float ZScale = 1f;

        [Space(5)]
        [Header("Audio Frequency Subsets")]
        [SerializeField] AudioFrequencySubsets xFrequence;
        [SerializeField] AudioFrequencySubsets yFrequence;


        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (mode == Mode.Turf)
                UpdateScaleTurf();
            else if (mode == Mode.Lerp)
                UpdateScaleLerp();
            else
                return;
        }


        void UpdateScaleTurf()
        {
            rect.localScale = new Vector3((AudioPeer.Instance.FreqBand[(int)xFrequence] * SizePower) + MinXScale,
                (AudioPeer.Instance.FreqBand[(int)yFrequence] * SizePower) + MinYScale,
                ZScale);
        }

    
        private Vector3 targetScale;
        private Vector3 currentVelocity = Vector3.zero;
        float targetX;
        float targetY;
        void UpdateScaleLerp()
        {
            targetX = (AudioPeer.Instance.FreqBand[(int)xFrequence] * SizePower) + MinXScale;
            targetY = (AudioPeer.Instance.FreqBand[(int)yFrequence] * SizePower) + MinYScale;
            targetScale = new Vector3(targetX, targetY, ZScale);

            rect.localScale = Vector3.SmoothDamp(rect.localScale, targetScale, ref currentVelocity, smoothTime);
        }
    }
}