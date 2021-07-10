using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Tarret;

public class HandleInput : MonoBehaviour
{
    [Inject]
    ITarretState tarret;
    [SerializeField] GameObject tarretCart;
    [SerializeField] float cartSpeed = 1.0f;
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

    public void CartMove()
    {

    }
}
