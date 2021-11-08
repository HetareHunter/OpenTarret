using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteRotateArrow : MonoBehaviour
{
    [SerializeField] GameObject RayPointer;
    PointerRayCaster pointerRayCaster;
    //[SerializeField] GameObject anglePoint;
    [SerializeField, Range(0, 1)] float rotateSpeed = 0.5f;

    private void Start()
    {
        pointerRayCaster = RayPointer.GetComponent<PointerRayCaster>();
    }
    private void FixedUpdate()
    {
        RotateArrow();
    }

    void RotateArrow()
    {
        Quaternion targetRotation = Quaternion.LookRotation(pointerRayCaster.hitPointerObj - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);
    }
}
