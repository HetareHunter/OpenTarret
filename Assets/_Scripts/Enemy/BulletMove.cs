using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] int damage = 10;
    Rigidbody m_rb;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.velocity = transform.forward*speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Tarret"))
        {
            other.transform.parent.GetComponent<TarretVitalManager>().TarretDamage(damage);
            
        }

        gameObject.SetActive(false);
    }
}
