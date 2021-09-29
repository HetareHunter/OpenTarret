using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleCreater : MonoBehaviour
{
    public GameObject _particlePrefab;
    public int _particleObjMax = 10;
    public GameObject _ObejctPoolObj;
    protected ObjectPool _objectPool;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _objectPool = _ObejctPoolObj.GetComponent<ObjectPool>();
    }
    public virtual void InstanceParticle()
    {
        var poolParticle = _objectPool.GetObject(_particlePrefab);
    }
}