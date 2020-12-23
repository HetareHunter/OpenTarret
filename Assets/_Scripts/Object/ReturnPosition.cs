using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    OVRGrabbable grabbable;
    //bool isGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Released();
    }

    private void Released()
    {
        if (!grabbable.isGrabbed)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    //private void Grabbed()
    //{
    //    if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
    //    {
    //        startPosition = grabber.grabbedObject.gameObject.transform.position;
    //        startRotation = grabber.grabbedObject.gameObject.transform.rotation;
    //    }
    //}

}
