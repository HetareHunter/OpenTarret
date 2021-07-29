using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;

public class PointerGunInputter : MonoBehaviour
{
    [SerializeField] GameObject tarret;
    TarretStateManager tarretStateManager;

    private void Start()
    {
        tarretStateManager = tarret.GetComponent<TarretStateManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            tarretStateManager.ChangeTarretState(TarretState.Rotate);
        }

        if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            tarretStateManager.ChangeTarretState(TarretState.Idle);
        }
    }
}
