using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public interface IGrabbable
    {
        void GrabBegin(OVRInput.Controller controller);
    }
}