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
        InvaderDeath InvaderDeath;
        InvaderMover invaderMover;
        InvaderCounter invaderCounter;
        InvaderGameStateManager InvaderGameStateManager;
        GameObject gameManager;
        CapsuleCollider capsuleCollider;
        Animator animator;

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
            InvaderDeath = GetComponent<InvaderDeath>();
            animator = GetComponent<Animator>();
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
            //以前の状態を保持
            //var prev = gameState;
            //次の状態に変更する
            invaderState = next;
            // ログを出す
            //Debug.Log($"ChangeState {prev} -> {next}");

            switch (invaderState)
            {
                case InvaderState.Standby:
                    invaderMover.March(false);
                    capsuleCollider.enabled = false;
                    animator.SetTrigger("ToIdle");
                    break;
                case InvaderState.March:
                    invaderMover.March(true);
                    capsuleCollider.enabled = true;
                    animator.SetTrigger("ToWalk");
                    break;
                case InvaderState.Death:
                    invaderMover.March(false);
                    animator.SetTrigger("ToDie");
                    //capsuleCollider.enabled = false;
                    if (invaderCounter == null)
                    {
                        invaderCounter = GetComponentInParent<InvaderCounter>();
                    }
                    invaderCounter.CountInvader(-1);
                    break;
                case InvaderState.Reset:
                    //初期状態にする
                    invaderMover.March(false);
                    capsuleCollider.enabled = false;
                    animator.SetTrigger("ToIdle");
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
                //ボーダーラインを越えてインベーダーが迫って来ているので敗北条件を満たした
                InvaderGameStateManager.FinishGame(false);
            }
            capsuleCollider.enabled = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Explodable"))
            {
                capsuleCollider.enabled = false;
                InvaderDeath.OnDead();
            }
        }
    }
}