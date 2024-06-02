using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

namespace FrequencyBand
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject cube;
        [SerializeField] private GameObject paramCube;
        private GameObject[] cubes;
        private GameObject[] paramCubes;

        enum Mode {
            Radius = 0,
            Linear =1,
        }

        [SerializeField] Mode mode; 

        [Space(3)]
        [Header("[Radius Cubes Setting]")]
        [SerializeField] float spawnRadius = 10f;
        [Range(0f, 100f)]
        [SerializeField] float sizePower = 20f;
        [Range(0, 2)]
        [SerializeField] float minYScale = 1.0f;
        [Range(0, 1)]
        [SerializeField] float rXScale = 0.1f;
        [Range(0, 1)]
        [SerializeField] float rZScale = 0.1f;
        [SerializeField] float increment = 1.0f;

        [Space(3)]
        [Header("[Param Cubes Setting]")]
        [Range(0f, 100f)]
        [SerializeField] float pSizePower = 20f;
        [Range(0, 2)]
        [SerializeField] float pMinYScale = 1.0f;
        [Range(0, 1)]
        [SerializeField] float pXScale = 0.5f;
        [Range(0, 1)]
        [SerializeField] float pZScale = 0.5f;
        int SampleLength;
        int ParamLength;


        bool spawndRadius;
        bool spawndParam;

        private void Start()
        {
            SampleLength = AudioPeer.Instance.Samples.Length;
            ParamLength = AudioPeer.Instance.FreqBand.Length;
            switch (mode)
            {
                case Mode.Radius:
                    SpawnCubeRadius();
                    break;
                case Mode.Linear:
                    SpawnCubeLinear();
                    break;  
            }
            SpawnParamCubes();
        }

        private void Update()
        {
            if (spawndRadius) 
                UpdateRadius();

            if (spawndParam)
                UpdateParams();
        }

        void UpdateRadius()
        {
            for (int i = 0; i < SampleLength; i++)
            {
                cubes[i].transform.localScale = new Vector3(rXScale, (AudioPeer.Instance.Samples[i] * sizePower) + minYScale, rZScale);
            }
        }

        void UpdateParams()
        {
            for(int i = 0; i < ParamLength ; i++)
            {
                paramCubes[i].transform.localScale = new Vector3(pXScale, (AudioPeer.Instance.FreqBand[i] * pSizePower) + pMinYScale, pZScale);
            }
        }

        Vector3 dir;
        Vector3 pos;

        void SpawnCubeRadius()
        {
            cubes = new GameObject[SampleLength];
            GameObject newParent = new GameObject("Radius Cubes");
            newParent.transform.parent = transform;


            for (int i = 0; i < SampleLength; i++)
            {
                float angle = i * 360f / SampleLength;
                dir = Quaternion.Euler(0, angle, 0) * transform.forward;
                pos = transform.position + dir * spawnRadius;

                GameObject newCube = Instantiate(cube, pos, Quaternion.identity, newParent.transform);
                cube.name = $"Cube {i}";
                cubes[i] = newCube;
            }
            spawndRadius = true;
        }


        void SpawnCubeLinear()
        {
            cubes = new GameObject[SampleLength];
            GameObject newParent = new GameObject(" Cubes");
            newParent.transform.parent = transform;

            for (int i = 0; i < SampleLength; i++)
            {
                Vector3 pos = transform.position + new Vector3(increment * i, 0f, 0f);

                GameObject newCube = Instantiate(cube, pos, Quaternion.identity, newParent.transform);
                cube.name = $"Cube {i}";
                cubes[i] = newCube;
            }
            spawndRadius = true;
        }

        void SpawnParamCubes()
        {
            paramCubes = new GameObject[ParamLength];
            GameObject newParent = new GameObject("ParamCubes");
            newParent.transform.parent = transform;

            for (int i = 0; i < ParamLength; i++)
            {
                GameObject newCube = Instantiate(paramCube, new Vector3((-ParamLength / 2f) + i * 1.3f, 0, 0), Quaternion.identity,newParent.transform);
                newCube.name = $"ParamCube {i}";
                paramCubes[i] = newCube;
            }
            spawndParam = true;
        }
    }
}