using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerObject", menuName = "Spawners/Poisson", order = 1)]
public class PoissonSpawnerData : SpawnerData
{

    [Header("Setting")]
    public float MinDistance;
    public Vector2 RegionSize;
    public int NumSamplesBeforeRejection;

}
