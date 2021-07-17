using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderMoveCommander : IInvaderMoveCommandable
    {
        InvaderGenerator invaderGenerator;
        InvaderMover[] invaderMovers;
        //private void Start()
        //{
        //    invaderGenerator = GetComponent<InvaderGenerator>();
        //}

        public void SetInvaders(List<GameObject> invaders)
        {
            for (int i = 0; i < invaders.Count; i++)
            {
                invaderMovers[i] = invaders[i].GetComponent<InvaderMover>();
            }
        }

        public void LimitMove()
        {
            foreach (var item in invaderMovers)
            {
                item.LimitMove();
            }
        }
    }
}