using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPower : MonoBehaviour
{
    [SerializeField] float power;
    Rigidbody m_rb;
    [SerializeField] TarretAttackData tarretAttackData;
    float deathTime;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        deathTime = tarretAttackData.explodeExistTime;
    }
    private void OnEnable()
    {
        m_rb.AddForce(transform.forward * power);
        Invoke("DeathBeamPower", deathTime);
    }

    void DeathBeamPower()
    {
        gameObject.SetActive(false);
    }
}
