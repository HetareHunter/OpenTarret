using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPositionCameraAimArrow : MonoBehaviour
{
    [SerializeField] GameObject rotatePosi;
    [SerializeField, Range(0, 1)] float rotateSpeed = 1.0f;
    private void FixedUpdate()
    {
        RotateArrow();
    }

    void RotateArrow()
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotatePosi.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);
    }
}
