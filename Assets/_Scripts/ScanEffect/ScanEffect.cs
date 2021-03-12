using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキャンしていくオブジェクトに付ける。板状のもの
/// </summary>
public class ScanEffect : MonoBehaviour
{
    BoxCollider boxCollider;

    [SerializeField] GameObject scanParticle;

    [SerializeField] float effectLifeTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var point in collision.contacts)
        {
            GameObject effect = Instantiate(scanParticle, point.point, Quaternion.identity);
            Destroy(effect, effectLifeTime);
        }
    }
}
