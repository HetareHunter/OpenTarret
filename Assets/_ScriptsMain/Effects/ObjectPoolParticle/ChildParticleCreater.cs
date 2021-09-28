using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ChildParticleCreater : ParticleCreater
{
    [SerializeField] GameObject _parentObj;

    protected override void Awake()
    {
        base.Awake();
        _objectPool.CreatePool(_particlePrefab, _particleObjMax, _parentObj.transform);
    }
    public override void InstanceParticle()
    {
        var poolParticle = _objectPool.GetObject(_parentObj.transform);
    }
}
