using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ParticleCreater : MonoBehaviour
{
    [SerializeField] GameObject _particlePrefab;
    [SerializeField] GameObject _parentObj;
    [SerializeField] int _particleObjMax = 10;
    ObjectPool _objectPool;
    // Start is called before the first frame update
    void Awake()
    {
        _objectPool = GetComponent<ObjectPool>();
        if (_parentObj != null)
        {
            _objectPool.CreatePool(_particlePrefab, _particleObjMax, _parentObj.transform);
        }
        else
        {
            _objectPool.CreatePool(_particlePrefab, _particleObjMax);
        }
        
    }

    public void InstanceParticle()
    {
        var poolParticle = _objectPool.GetObject(_parentObj.transform);
    }
}
