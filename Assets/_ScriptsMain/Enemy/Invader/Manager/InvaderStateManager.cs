using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum InvaderState
    {
        Standby,
        March,
        Death,
    }

    public class InvaderStateManager : MonoBehaviour
    {
        public InvaderState invaderState = InvaderState.Standby;
        InvaderMover invaderMover;
        InvaderCounter invaderCounter;
        // Start is called before the first frame update
        void Start()
        {
            invaderMover = GetComponent<InvaderMover>();
            invaderCounter = GetComponentInParent<InvaderCounter>();
        }

        public void ChangeInvaderState(InvaderState next)
        {
            if (invaderMover == null)
            {
                invaderMover = GetComponent<InvaderMover>();
            }
            //�ȑO�̏�Ԃ�ێ�
            //var prev = gameState;
            //���̏�ԂɕύX����
            invaderState = next;
            // ���O���o��
            //Debug.Log($"ChangeState {prev} -> {next}");

            switch (invaderState)
            {
                case InvaderState.Standby:
                    invaderMover.OnMarch = false;
                    break;
                case InvaderState.March:
                    invaderMover.OnMarch = true;
                    break;
                case InvaderState.Death:
                    invaderMover.OnMarch = false;
                    if (invaderCounter == null)
                    {
                        invaderCounter = GetComponentInParent<InvaderCounter>();
                    }
                    invaderCounter.CountInvader(-1);
                    break;
                default:
                    break;
            }
        }
    }
}