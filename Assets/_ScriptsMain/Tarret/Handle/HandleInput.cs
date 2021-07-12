using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Tarret;

namespace Players
{
    public class HandleInput : MonoBehaviour
    {
        [Inject]
        ITarretState tarret;
        [SerializeField] GameObject tarretCart;
        [SerializeField] float cartSpeed = 1.0f;
        private void Start()
        {
        }

        public void Attack()
        {
            tarret.ChangeTarretState(TarretCommand.Attack);
        }

        public void CartMove(Vector2 stick)
        {

            tarretCart.transform.Translate(Vector3.right * stick.x * cartSpeed * Time.deltaTime);
        }
    }
}