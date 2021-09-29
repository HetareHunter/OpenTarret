using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldParticleCreater : ParticleCreater
{
    protected override void Awake()
    {
        base.Awake();
        _objectPool.CreatePool(_particlePrefab, _particleObjMax);
    }

    public void InstanceParticle(Vector3 position, Quaternion angle)
    {
        var poolParticle = _objectPool.GetObject(_particlePrefab, position, angle);
    }
}
