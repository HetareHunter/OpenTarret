using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    public int power = 10;
    Rigidbody m_rb;

    private void Awake()
    {
        
    }


    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.velocity = transform.forward*speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        gameObject.SetActive(false);
    }
}
