using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionSpawner : Spawner
{

    [Header("Center point")]
    public Transform CenterOfSpawning;
    public float MaxSpawnDistanceFromCenter;
    public float MinSpawnDistanceFromCenter;

    [Header("Burst")]
    public int BurstCount;
    public int BurstSize;

    public override IEnumerator SpawningRoutine()
    {
        for(int count = 0; count < BurstCount; count++)
        {
            for(int size = 0; size < BurstSize; size++) { 
                GameObject spawnedObject = Spawn(this.transform.position, Quaternion.identity, this.transform);

                if(spawnedObject == null)
                {
                    break;
                }

                spawnedObject.SetActive(true);
            }
        }

        yield return new WaitForSeconds(_spawningRate);
        StartCoroutine(SpawningRoutine());      
    }
}
