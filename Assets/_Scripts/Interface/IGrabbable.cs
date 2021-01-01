using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void GrabBegin(OVRInput.Controller controller);
}
