using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGrab : OVRGrabbable, IGrabbable
{
    OVRInput.Controller currentController;

    ReturnPosition returnPosition = new ReturnPosition();
    [SerializeField] Transform m_rotateTarget;
    [SerializeField] Transform m_handle;
    [SerializeField, Range(0, 50.0f)] float handleRotateLimit;
    Vector3 m_preHandlePosi;
    float rotateAngle;
    /// <summary>
    /// ハンドルの感度
    /// </summary>
    [SerializeField] float handleSensitivity = 2.0f;


    public void GrabBegin(OVRInput.Controller controller)
    {
        currentController = controller;
    }


    protected override void Start()
    {
        returnPosition = GetComponent<ReturnPosition>();
        m_preHandlePosi = m_rotateTarget.position;
    }
    void Update()
    {
        if (isGrabbed && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController))
        {
            // implement

        }

        if (isGrabbed)
        {
            RotateHandle();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, currentController))
        {
            returnPosition.Released();
            ResetRotateHandle();
        }
        Debug.Log("currentcontroller:" + currentController);
    }

    void RotateHandle()
    {
        float handleMoveDistance = (m_preHandlePosi.z - m_rotateTarget.position.z) / Time.deltaTime * handleSensitivity;
        rotateAngle += handleMoveDistance;
        rotateAngle = Mathf.Clamp(rotateAngle, -handleRotateLimit, handleRotateLimit);
        m_handle.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
        m_preHandlePosi = m_rotateTarget.position;
    }

    void ResetRotateHandle()
    {
        rotateAngle = 0;
        m_handle.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
        m_preHandlePosi = m_rotateTarget.position;
    }
}