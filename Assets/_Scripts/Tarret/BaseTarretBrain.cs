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
        TarretAttack tarretAttack;

        /// <summary>バイクのブレーキのあそびと同じ意味 </summary>
        public float m_commandPlay = 0.1f;

        public TarretCommand tarretCommandState = TarretCommand.Idle;

        private void Start()
        {
            tarretFunction = GetComponent<BaseTarretRotateFunction>();
            tarretAttack = GetComponent<TarretAttack>();
        }

        /// <summary>
        /// タレットのコントローラの傾きでTarretCommandのstateを変化させる
        /// </summary>
        public void JudgeTarretCommandState()
        {
            if (Mathf.Abs(leftHandle.HandleRotatePer) > m_commandPlay && Mathf.Abs(rightHandle.HandleRotatePer) > m_commandPlay)
            {
                if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay ||
                    leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay)
                {
                    ChangeTarretState(TarretCommand.HorizontalRotate);
                }
                else if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay ||
                    leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay)
                {
                    ChangeTarretState(TarretCommand.VerticalRotate);
                }
            }
            else
            {
                //ChangeTarretState(TarretCommand.Idle);
            }

        }

        /// <summary>
        /// Tarretのステート変化を行う関数をここにまとめている
        /// </summary>
        /// <param name="next"></param>
        public void ChangeTarretState(TarretCommand next)
        {
            //以前の状態を保持
            var prev = tarretCommandState;
            //次の状態に変更する
            tarretCommandState = next;
            // ログを出す
            Debug.Log($"ChangeState {prev} -> {next}");

            switch (tarretCommandState)
            {
                case TarretCommand.Idle:
                    tarretFunction.SetVerticalRotateSpeed(leftHandle.transform, rightHandle.transform);
                    break;
                case TarretCommand.HorizontalRotate:
                    break;
                case TarretCommand.VerticalRotate:
                    break;
                case TarretCommand.Attack:
                    if(tarretAttack.attackable) tarretAttack.BeginAttack();
                    ChangeTarretState(TarretCommand.Idle);
                    break;
                default:
                    break;
            }
        }
    }
}