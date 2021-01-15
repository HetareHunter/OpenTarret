using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Managers
{
    public enum TarretCommand
    {
        Idle,
        HorizontalRotate,
        VerticalRotate,
        Attack,

    }

    /// <summary>
    /// タレットのステートを管理するクラス
    /// </summary>
    public class BaseTarretBrain : MonoBehaviour
    {
        public HandleGrabbable leftHandle;
        public HandleGrabbable rightHandle;

        BaseTarretRotateFunction tarretFunction;

        /// <summary>バイクのブレーキのあそびと同じ意味 </summary>
        public float m_commandPlay = 0.1f;

        public TarretCommand tarretCommanfState = TarretCommand.Idle;

        private void Start()
        {
            tarretFunction = GetComponent<BaseTarretRotateFunction>();
        }

        public void JudgeTarretCommandState()
        {
            if (!leftHandle.isGrabbed || !rightHandle.isGrabbed) ChangeTarretCommandIdle();

            if (Mathf.Abs(leftHandle.HandleRotatePer) > m_commandPlay && Mathf.Abs(rightHandle.HandleRotatePer) > m_commandPlay)
            {
                if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay ||
                    leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay)
                {
                    ChangeTarretCommandHorizontalRotate();
                }
                else if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay ||
                    leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay)
                {
                    ChangeTarretCommandVerticalRotate();
                }
            }
            else
            {
                ChangeTarretCommandIdle();
            }

        }

        void ChangeTarretCommandIdle()
        {
            tarretCommanfState = TarretCommand.Idle;
            tarretFunction.SetVerticalRotateSpeed(leftHandle.transform, rightHandle.transform);
        }

        void ChangeTarretCommandHorizontalRotate()
        {
            tarretCommanfState = TarretCommand.HorizontalRotate;
        }

        void ChangeTarretCommandVerticalRotate()
        {
            tarretCommanfState = TarretCommand.VerticalRotate;
        }

        void ChangeTarretCommandAttack()
        {
            tarretCommanfState = TarretCommand.Attack;
        }
    }
}