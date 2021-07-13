using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawLine : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject rayOfOrigin;
    void FixedUpdate()
    {
        Debug.DrawLine(rayOfOrigin.transform.position, rayOfOrigin.transform.position + rayDirection * rayDistance, Color.red);
    }
    private void Update()
    {
        Debug.DrawLine(rayOfOrigin.transform.position, rayDirection * rayDistance, Color.red);
    }
}
