using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{

    [SerializeField]
    ObjectPool _objectPool;

    [SerializeField]
    protected float _spawningRate;

    [SerializeField]
    protected bool _spawnFromStart;



    public virtual void Start()
    {
        if (_spawnFromStart)
        {
            StartCoroutine(SpawningRoutine());
        }
    }



    protected GameObject Spawn(Vector3 position, Quaternion rotation)
    {

        GameObject availableObject = _objectPool.GetAvailableObject();

        if(availableObject == null)
        {
            return null;
        }

        availableObject.transform.position = position;
        availableObject.transform.rotation = rotation;
        return availableObject;

    }

    protected GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject availableObject = _objectPool.GetAvailableObject();

        if (availableObject == null)
        {
            return null;
        }


        availableObject.transform.parent = parent;
        availableObject.transform.position = position;
        availableObject.transform.rotation = rotation;
        return availableObject;
    }

    public abstract IEnumerator SpawningRoutine();
    
   
}
