using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MoveDirection
{
    Left,
    Right,
}

namespace Enemy
{

    public class InvaderMover : MonoBehaviour
    {
        /// <summary>
        /// �ړ�����܂ł̃t���[����
        /// </summary>
        public int movePeriodOfFrame = 90;
        public MoveDirection moveDirection = MoveDirection.Left;
        int frame = 0;
        Vector3 currentPosi;
        bool isAlive = true;
        public bool onVerticalMove = false;
        /// <summary>
        /// ���͊�{8�ɂ��Ƃ���
        /// </summary>
        [SerializeField] int limitPosi = 8;
        InvaderMoveCommander moveCommander;



        // Start is called before the first frame update
        void Start()
        {
            currentPosi = gameObject.transform.position;
            moveCommander = GetComponentInParent<InvaderMoveCommander>();
        }

        void FixedUpdate()
        {
            if (isAlive)
            {
                
                if (IsOverLimitPosition() && !onVerticalMove)
                {
                    moveCommander.LimitMove();
                }
                Move();
            }
        }

        public bool IsOverLimitPosition()
        {
            if (moveDirection == MoveDirection.Right)
            {
                if (transform.localPosition.x >= limitPosi)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (transform.localPosition.x <= -limitPosi)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        void Move()
        {
            frame++;
            if (frame % movePeriodOfFrame == 0)
            {
                if (onVerticalMove)
                {
                    VerticalMove();
                    onVerticalMove = false;
                }
                else
                {
                    HorizontalMove();
                }
                currentPosi = transform.position;
            }
        }

        private void HorizontalMove()
        {
            if (moveDirection == MoveDirection.Left)
            {
                transform.position = currentPosi - transform.right;
            }
            else
            {
                transform.position = currentPosi + transform.right;
            }
        }

        void VerticalMove()
        {
            transform.position = currentPosi + transform.forward;
        }

        void ChangeMoveDirection()
        {
            if (moveDirection == MoveDirection.Right)
            {
                moveDirection = MoveDirection.Left;
            }
            else
            {
                moveDirection = MoveDirection.Right;
            }
        }

        /// <summary>
        /// �w���x���W�ȏ�ȉ���x���W���B�����Ƃ��A������ς���
        /// </summary>
        /// <param name="limitPosi"></param>
        public void LimitMove()
        {
            ChangeMoveDirection();
            onVerticalMove = true;
        }

    }
}