using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSpawner : Spawner
{
    

    public override IEnumerator SpawningRoutine()
    {
        Vector3 position = Vector3.zero;
        Transform transform = this.transform;
        Quaternion rotation = Quaternion.identity;             
        
        
        
        Spawn(position, rotation);
        throw new System.NotImplementedException();
    }
}
