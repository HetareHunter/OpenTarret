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

    }
    public class BaseTarretBrain : MonoBehaviour
    {
        [SerializeField] HandleGrabbable leftHandle;
        [SerializeField] HandleGrabbable rightHandle;

        /// <summary>バイクのブレーキのあそびと同じ意味 </summary>
        public float m_commandPlay = 0.1f;

        public TarretCommand tarretCommanfState = TarretCommand.Idle;
        
        public void JudgeTarretCommandState()
        {
            if (Mathf.Abs(leftHandle.HandleRotatePer) > m_commandPlay && Mathf.Abs(rightHandle.HandleRotatePer) < -m_commandPlay)
            {
                if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay ||
                    leftHandle.HandleRotatePer < m_commandPlay && rightHandle.HandleRotatePer > -m_commandPlay)
                {
                    ChangeTarretCommandHorizontalRotate();
                }
                else if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer > -m_commandPlay ||
                    leftHandle.HandleRotatePer < m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay)
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
        }

        void ChangeTarretCommandHorizontalRotate()
        {
            tarretCommanfState = TarretCommand.HorizontalRotate;
        }

        void ChangeTarretCommandVerticalRotate()
        {
            tarretCommanfState = TarretCommand.VerticalRotate;
        }
    }
}