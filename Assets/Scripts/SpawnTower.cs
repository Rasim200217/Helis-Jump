using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] private int _levelCount;
    [SerializeField] private float _additionScale;
    [SerializeField] private GameObject _beam;
    [SerializeField] private StartPlatform _startPlatform;
    [SerializeField] private Platform[] _platform;
    [SerializeField] private EndPlatform _endPlatform;

    private float _startAndFinishAdditionScale = 0.5f;

    public float beamScaleY => _levelCount / 2f + _startAndFinishAdditionScale + _additionScale / 2f;


    private void Start()
    {
        Build();
    }

    private void Build()
    {
        GameObject beam = Instantiate(_beam, transform);
        // ReSharper disable once PossibleLossOfFraction
        beam.transform.localScale = new Vector3(1, beamScaleY, 1);

        Vector3 spawnPosition = _beam.transform.position;
        spawnPosition.y += _beam.transform.localScale.y - _additionScale;
        
        SpawnPlatform(_startPlatform, ref spawnPosition, beam.transform);
        
        for (int i = 0; i < _levelCount; i++)
        {
          SpawnPlatform(_platform[UnityEngine.Random.Range(0, _platform.Length)], ref spawnPosition, beam.transform);
        }
        
        SpawnPlatform(_endPlatform, ref spawnPosition, beam.transform);
    }

    private void SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
        Instantiate(platform, spawnPosition, Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0), parent);
        spawnPosition.y -= 1;
    }
}
