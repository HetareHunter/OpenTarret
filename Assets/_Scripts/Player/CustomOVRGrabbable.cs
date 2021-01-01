using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOVRGrabbable : OVRGrabber
{
    protected override void GrabBegin()
    {
        base.GrabBegin();
        if (m_grabbedObj is IGrabbable)
        {
            ((IGrabbable)m_grabbedObj).GrabBegin(m_controller);
        }
    }
}