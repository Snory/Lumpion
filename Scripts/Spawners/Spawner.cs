using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{

    [SerializeField]
    ObjectPool _objectPool;

    [SerializeField]
    float _spawningTime;


    protected virtual GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        return Instantiate(_objectPool.GetAvailableObject(), position, rotation);
    }

    public virtual GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(_objectPool.GetAvailableObject(), position, rotation, parent);  
    }

    public abstract IEnumerator SpawningRoutine();
    
   
}
