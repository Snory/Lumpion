using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoisonSampledSpawner : Spawner
{
    [SerializeField]
    protected PoissonSpawnerData _spawnerData;

    public void Awake()
    {
        base.SetSpawnerData(_spawnerData);
        _spawnedObjects = new List<GameObject>();
    }


    public override IEnumerator SpawningRoutine()
    {
          
        foreach (var spawn in _spawnedObjects.Where(s => s.activeInHierarchy))
        {
            spawn.SetActive(false); 
        }

        _spawnedObjects = GetPoissonSamples();  
        yield return new WaitForSeconds(_spawnerData.SpawnRate);
        StartCoroutine(SpawningRoutine());
    }

 
    private List<GameObject> GetPoissonSamples()
    {
        List<GameObject> spawnedSamples = new List<GameObject>();

        float minDistance = _spawnerData.MinDistance;

        float cellSize = minDistance / Mathf.Sqrt(2);


        int[,] grid = new int[Mathf.CeilToInt(_spawnerData.RegionSize.x / cellSize), Mathf.CeilToInt(_spawnerData.RegionSize.y / cellSize)];
        List<Vector2> adequateSamples = new List<Vector2>();
        Queue<Vector2> activeSamples = new Queue<Vector2>();

        activeSamples.Enqueue(_spawnerData.RegionSize / 2);

        while (activeSamples.Count > 0)
        {
            Vector2 spawnCenter = activeSamples.Dequeue();

            for (int i = 0; i < _spawnerData.NumSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;

                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                float distance = Random.Range(minDistance, minDistance * 2);
                Vector2 candidate = spawnCenter + dir * distance;

                if (IsValid(candidate, cellSize, adequateSamples, grid))
                {

                    GameObject spawnedObject = Spawn(new Vector2(candidate.x - _spawnerData.RegionSize.x / 2 + this.transform.position.x, candidate.y - _spawnerData.RegionSize.y / 2 + this.transform.position.y), Quaternion.identity);

                    if(spawnedObject == null)
                    {
                        break;
                    }

                    spawnedSamples.Add(spawnedObject);
                    spawnedObject.SetActive(true);
                    spawnedObject.transform.parent = this.transform;
                    adequateSamples.Add(candidate);                                     

                    activeSamples.Enqueue(candidate);
                    activeSamples.Enqueue(spawnCenter);                    
                    
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = adequateSamples.Count;
                    break;
                }
            }
        }

        return spawnedSamples;
    }

    private bool IsValid(Vector2 candidate, float cellSize, List<Vector2> points, int[,] grid)
    {

        if (candidate.x <= 0 || candidate.x > _spawnerData.RegionSize.x || candidate.y <= 0 || candidate.y > _spawnerData.RegionSize.y)
        {
            return false;
        }

        int cellX = (int)(candidate.x / cellSize);
        int cellY = (int)(candidate.y / cellSize);
        int searchStartX = Mathf.Max(0, cellX - 2);
        int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
        int searchStartY = Mathf.Max(0, cellY - 2);
        int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

        for (int x = searchStartX; x <= searchEndX; x++)
        {
            for (int y = searchStartY; y <= searchEndY; y++)
            {

                int pointIndex = grid[x, y] - 1;
                if (pointIndex != -1)
                {
                    float sqrDistance = (candidate - points[pointIndex]).sqrMagnitude;
                    if (sqrDistance < _spawnerData.MinDistance * _spawnerData.MinDistance)
                    {
                        return false;
                    }
                }
            }
        }

        return true;

    }
}
