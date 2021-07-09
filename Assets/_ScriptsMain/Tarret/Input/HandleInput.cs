using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Tarret;

public class HandleInput : MonoBehaviour
{
    [Inject]
    ITarretState tarret;
    private void Start()
    {
    }

    public void Attack(OVRInput.Controller currentController)
    {
        if (transform.tag == "RHundle")
        {
            tarret.ChangeTarretState(TarretCommand.Attack);
        }
    }
}
