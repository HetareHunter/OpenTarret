using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float activeTime = 3.0f;
    public float power = 10;
    Rigidbody m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    //private void OnEnable()
    //{

    //}

    public void Fire()
    {
        m_rb.velocity = transform.forward * speed;
        Invoke("NotActive", activeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        NotActive();
    }

    private void OnCollisionEnter(Collision collision)
    {
        NotActive();
    }

    void NotActive()
    {
        m_rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}
