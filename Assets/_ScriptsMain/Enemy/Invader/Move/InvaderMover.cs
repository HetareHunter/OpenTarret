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
        int movePeriodOfFrame = 90;
        public MoveDirection moveDirection = MoveDirection.Left;
        int frame = 0;
        Vector3 currentPosi;
        public bool isActive = true;
        public bool OnMarch = false;
        public bool onVerticalMove = false;
        /// <summary>
        /// 今は基本8にしとこう
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
            if (OnMarch)
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

        public void ChangeMoveSpeed(int speed) 
        {
            movePeriodOfFrame = speed;
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

        public void March(bool activate)
        {
            OnMarch = activate;
        }
    }
}