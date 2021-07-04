using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class CustomOVRGrabber : OVRGrabber
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
}