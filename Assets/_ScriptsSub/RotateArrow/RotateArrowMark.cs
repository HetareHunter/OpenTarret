using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrowMark : MonoBehaviour
{
    [SerializeField] GameObject anglePoint;
    [SerializeField, Range(0, 1)] float rotateSpeed = 0.5f;

    private void FixedUpdate()
    {
        RotateArrow();
    }

    void RotateArrow()
    {
        Quaternion targetRotation = Quaternion.LookRotation(anglePoint.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);
    }
}
