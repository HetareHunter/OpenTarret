using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    [SerializeField] OVRGrabber oVRGrabberRight;
    [SerializeField] OVRGrabber oVRGrabberLeft;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (oVRGrabberRight.grabbedObject != gameObject && oVRGrabberLeft.grabbedObject != gameObject)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }


}
