using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleSystemEmitPosition : MonoBehaviour
{
    [SerializeField] Vector3 setPosi;
    [SerializeField] ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangePosition()
    {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = setPosi;
        emitParams.applyShapeToPosition = true;
        particle.Emit(emitParams, 50);
    }
}
