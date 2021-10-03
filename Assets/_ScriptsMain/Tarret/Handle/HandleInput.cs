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
        [SerializeField] GameObject tarretCartObj;
        TarretCartMover tarretCart;
        
        private void Start()
        {
            if (tarretCartObj != null)
            {
                tarretCart = tarretCartObj.GetComponent<TarretCartMover>();
            }
        }

        public void Attack()
        {
            tarret.ChangeTarretState(TarretState.Attack);
        }

        public void CartMove(Vector2 stick)
        {
            if (tarretCart == null) return;
            tarretCart.CartMove(stick);
            //tarretCart.transform.Translate(Vector3.right * stick.x * cartSpeed * Time.deltaTime);
        }
    }
}