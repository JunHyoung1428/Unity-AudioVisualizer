using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private GameObject[] cubes;

    [Header("Settings")]
    [SerializeField] float spawnRadius = 30f;
    [SerializeField] float sizePower  =10f;

    [Space(2)]
    [Header("Scales")]
    [Range(0,2)]
    [SerializeField] float minYScale = 1.0f;
    [SerializeField] float xScale = 1f;
    [SerializeField] float zScale = 1f;
    int SampleLength;
    bool spawned;



    private void Start()
    {
        SampleLength = AudioPeer.Instance.Samples.Length;
        SpawnCube();
    }

    private void Update()
    {
        if (!spawned) return;

        for(int i = 0; i < SampleLength; i++)
        {
            cubes[i].transform.localScale = new Vector3(xScale, (AudioPeer.Instance.Samples[i] * sizePower) + minYScale, zScale);
        }
    }

    Vector3 dir;
    Vector3 pos;

    void SpawnCube()
    {
        cubes = new GameObject[SampleLength];
        
        for(int i = 0; i < SampleLength; i++)
        {
            float angle = i * 360f / SampleLength;
            dir = Quaternion.Euler(0,angle,0)*transform.forward;
            pos = transform.position + dir * spawnRadius;

            GameObject newCube = Instantiate(cube, pos, Quaternion.identity, transform);
            cube.name = $"Cube {i}";
            cubes[i] = newCube;
        }
        spawned = true;
    }
}
