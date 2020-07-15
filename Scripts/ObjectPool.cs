using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabObject;

    [SerializeField]
    private int _poolDepth;

    [SerializeField]
    private bool _canGrow;

    private readonly List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolDepth; i++)
        {
            _pool.Add(CreateObjectForPool());
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }

        if (_canGrow)
        {
            return CreateObjectForPool();
        }

        return null;
    }

    private GameObject CreateObjectForPool()
    {
        GameObject pooledObject = Instantiate(_prefabObject);
        pooledObject.SetActive(false);
        pooledObject.transform.parent = this.transform;
        return pooledObject;
    }

}
