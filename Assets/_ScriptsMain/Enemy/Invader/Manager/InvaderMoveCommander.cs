using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderMoveCommander : MonoBehaviour
    {
        InvaderMover[] invaderMovers;
        InvaderStateManager[] invaderStateManager;

        public void SetInvaders(List<GameObject> invaders)
        {
            invaderMovers = new InvaderMover[invaders.Count];
            invaderStateManager = new InvaderStateManager[invaders.Count];
            for (int i = 0; i < invaders.Count; i++)
            {
                invaderMovers[i] = invaders[i].GetComponent<InvaderMover>();
                invaderStateManager[i] = invaders[i].GetComponent<InvaderStateManager>();
            }
        }

        public void LimitMove()
        {
            if (invaderMovers == null)
            {
                return;
            }
            foreach (var item in invaderMovers)
            {
                item.LimitMove();
            }
        }
         /// <summary>
         /// �i�R���閽�߂��o��
         /// </summary>
        public void CommenceMarch()
        {
            if (invaderStateManager == null)
            {
                return;
            }
            foreach (var item in invaderStateManager)
            {
                item.ChangeInvaderState(InvaderState.March);
            }
        }

        /// <summary>
        /// �����~�܂��Ă��閽�߂��o��
        /// </summary>
        public void CommenceStandby()
        {
            if (invaderStateManager == null)
            {
                return;
            }
            foreach (var item in invaderStateManager)
            {
                item.ChangeInvaderState(InvaderState.Standby);
            }
        }
    }
}