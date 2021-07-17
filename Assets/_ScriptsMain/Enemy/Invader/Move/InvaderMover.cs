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
        /// 移動するまでのフレーム数
        /// </summary>
        public int movePeriodOfFrame = 90;
        public MoveDirection moveDirection = MoveDirection.Left;
        int frame = 0;
        Vector3 currentPosi;
        bool isAlive = true;
        bool onVerticalMove = false;
        /// <summary>
        /// 今は基本8にしとこう
        /// </summary>
        [SerializeField] int limitPosi = 8;
        [Inject]
        IInvaderMoveCommandable invaderMoveCommandable;

        public bool IsOverLimitPosition()
        {
            if (moveDirection == MoveDirection.Right)
            {
                if (transform.position.x <= -limitPosi)
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
                if (transform.position.x >= limitPosi)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            currentPosi = gameObject.transform.position;
        }

        void FixedUpdate()
        {
            if (isAlive)
            {
                Move();
                if (IsOverLimitPosition())
                {
                    invaderMoveCommandable.LimitMove();
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
        /// 指定のx座標以上以下のx座標到達したとき、向きを変える
        /// </summary>
        /// <param name="limitPosi"></param>
        public void LimitMove()
        {
            ChangeMoveDirection();
            onVerticalMove = true;
        }

    }
}