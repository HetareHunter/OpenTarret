using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPower : MonoBehaviour
{
    [SerializeField] float power;
    Rigidbody m_rb;

    /// <summary>
    /// 物理的に与える力
    /// </summary>
    /// <param name="direction"></param>
    public void Movement(Vector3 direction)
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(direction * power);
    }
}
