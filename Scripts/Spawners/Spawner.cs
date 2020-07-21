using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Spawner : MonoBehaviour
{

    [SerializeField]
    private Pool _objectPool;


    protected List<GameObject> _spawnedObjects;

    protected SpawnerData _spawnerdata;

    protected virtual void Start()
    {
        if (_spawnerdata.SpawnFromStart)
        {
            StartCoroutine(SpawningRoutine());
        }
    }

    protected void SetSpawnerData(SpawnerData data)
    {
        _spawnerdata = data;
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
