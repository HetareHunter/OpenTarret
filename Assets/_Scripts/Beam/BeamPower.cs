using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPower : MonoBehaviour
{
    [SerializeField] float power;
    Rigidbody m_rb;

    public void Movement(Vector3 direction)
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(direction * power);
    }
}
