﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoisonSampledSpawner : Spawner
{

    [Header("Poisson sampling setting")]
    public float Radius;
    public Vector2 SampleSizeRegion;

    [SerializeField]
    private int _numSamplesBeforeRejection = 30;


    public override IEnumerator SpawningRoutine()
    {

        float cellSize = Radius / Mathf.Sqrt(2);
        int[,] grid = new int[Mathf.CeilToInt(SampleSizeRegion.x / cellSize), Mathf.CeilToInt(SampleSizeRegion.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(SampleSizeRegion / 2);
        while(spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0,spawnPoints.Count);
            Vector2 spawnCenter = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for (int i = 0; i < _numSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCenter + dir * Random.Range(Radius, 2 * Radius);

                if (IsValid(candidate, cellSize, points, grid))
                {
                    GameObject spawnedObject = Spawn(candidate, Quaternion.identity);
                    spawnedObject.SetActive(true);
                    spawnedObject.transform.parent = this.transform;

                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;

                }

            }

            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }

        }


        yield return new WaitForSeconds(_spawningRate);
        StartCoroutine(SpawningRoutine());      
    }

    private bool IsValid(Vector2 candidate, float cellSize, List<Vector2> points, int [,] grid)
    {

        if (candidate.x >= 0 && candidate.x < SampleSizeRegion.x && candidate.y >= 0 && candidate.y < SampleSizeRegion.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            for (int x = searchStartX; x < searchEndX; x++)
            {
                for(int y = searchStartY; y < searchEndY; y++)
                {

                    int pointIndex = grid[x, y] - 1;
                    if(pointIndex != -1)
                    {
                        float sqrDistance = (candidate - points[pointIndex]).sqrMagnitude;
                        if(sqrDistance < Radius * Radius)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;

        }
        return false;
    }
}
