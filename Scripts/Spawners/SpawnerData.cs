using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpawnerObject", menuName = "Spawners",  order = 1)]
public class SpawnerData : ScriptableObject
{
    public float SpawnRate;
    public bool SpawnFromStart;
}
