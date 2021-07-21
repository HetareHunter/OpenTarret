using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Enemy
{
    public enum InvaderState
    {
        Standby,
        March,
        Death,
        Reset,
    }

    public class InvaderStateManager : MonoBehaviour
    {
        Rigidbody m_rb;
        public InvaderState invaderState = InvaderState.Standby;
        InvaderMover invaderMover;
        InvaderCounter invaderCounter;
        InvaderGameStateManager InvaderGameStateManager;
        GameObject gameManager;
        CapsuleCollider capsuleCollider;

        // Start is called before the first frame update
        void Start()
        {
            invaderMover = GetComponent<InvaderMover>();
            invaderCounter = GetComponentInParent<InvaderCounter>();
            if (gameManager == null)
            {
                gameManager = GameObject.Find("GameManager");
            }
            InvaderGameStateManager = gameManager.GetComponent<InvaderGameStateManager>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            m_rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (invaderCounter == null)
            {
                invaderCounter = GetComponentInParent<InvaderCounter>();
            }
            invaderCounter.CountInvader(1);
        }

        public void ChangeInvaderState(InvaderState next)
        {
            if (invaderMover == null)
            {
                invaderMover = GetComponent<InvaderMover>();
            }
            if (capsuleCollider == null)
            {
                capsuleCollider = GetComponent<CapsuleCollider>();
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
                    invaderMover.March(false);
                    capsuleCollider.enabled = false;
                    break;
                case InvaderState.March:
                    invaderMover.March(true);
                    capsuleCollider.enabled = true;
                    break;
                case InvaderState.Death:
                    if (invaderCounter == null)
                    {
                        invaderCounter = GetComponentInParent<InvaderCounter>();
                    }
                    invaderCounter.CountInvader(-1);
                    break;
                case InvaderState.Reset:
                    //������Ԃɂ���
                    invaderMover.March(false);
                    capsuleCollider.enabled = false;
                    invaderMover.moveDirection = MoveDirection.Left;
                    StopInvader();
                    break;
                default:
                    break;
            }
        }

        public void StopInvader()
        {
            m_rb.velocity = Vector3.zero;
            m_rb.angularVelocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Finish"))
            {
                //�{�[�_�[���C�����z���ăC���x�[�_�[�������ė��Ă���̂Ŕs�k�����𖞂�����
                InvaderGameStateManager.FinishGame(false);
            }
            capsuleCollider.enabled = false;
        }
    }
}