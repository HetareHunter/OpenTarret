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
        Rotate,
        Break,
    }

    /// <summary>
    /// タレットのステートを管理するクラス
    /// </summary>
    public class BaseTarretBrain : MonoBehaviour
    {
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable leftHandle;
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable rightHandle;

        BaseTarretAttack tarretAttack;
        AnglePoint anglePoint;

        [SerializeField] GameObject tarretAnglePoint;
        bool anglePointPlayOneShot = false;

        public static TarretCommand tarretCommandState = TarretCommand.Idle;

        private void Start()
        {
            tarretAttack = GetComponent<BaseTarretAttack>();
            anglePoint = tarretAnglePoint.GetComponent<AnglePoint>();
        }

        ///// 
        ///// タレットのコントローラの傾きでTarretCommandのstateを変化させる
        ///// 
        //public void OldJudgeRotateTarret()
        //{
        //    if (Mathf.Abs(leftHandle.HandleRotatePer) > m_commandPlay && Mathf.Abs(rightHandle.HandleRotatePer) > m_commandPlay)
        //    {
        //        if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay ||
        //            leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay)
        //        {
        //            ChangeTarretState(TarretCommand.HorizontalRotate);
        //        }
        //        else if (leftHandle.HandleRotatePer > m_commandPlay && rightHandle.HandleRotatePer > m_commandPlay ||
        //            leftHandle.HandleRotatePer < -m_commandPlay && rightHandle.HandleRotatePer < -m_commandPlay)
        //        {
        //            ChangeTarretState(TarretCommand.VerticalRotate);
        //        }
        //    }
        //    else
        //    {
        //        //ChangeTarretState(TarretCommand.Idle);
        //    }

        //}

        /// <summary>
        /// タレットが回転するかどうかを判定する
        /// </summary>
        public void JudgeRotateTarret()
        {
            //両手ともタレットのハンドルを握っているとき
            if (leftHandle.isGrabbed && rightHandle.isGrabbed)
            {
                ChangeTarretState(TarretCommand.Rotate);
                if (anglePointPlayOneShot)
                {
                    anglePoint.BeginGrabHandle();
                    anglePointPlayOneShot = false;
                }
            }
            else
            {
                anglePointPlayOneShot = true;
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
            //// ログを出す
            Debug.Log($"ChangeState {prev} -> {next}");

            switch (tarretCommandState)
            {
                case TarretCommand.Idle:
                    //tarretFunction.SetVerticalRotateSpeed(leftHandle.transform, rightHandle.transform);
                    break;
                //case TarretCommand.HorizontalRotate:
                //    break;
                //case TarretCommand.VerticalRotate:
                //    break;
                case TarretCommand.Attack:
                    if (tarretAttack.attackable)
                    {
                        tarretAttack.BeginAttack();
                        leftHandle.AttackVibe();
                        rightHandle.AttackVibe();
                    }
                    ChangeTarretState(TarretCommand.Idle);
                    break;

                case TarretCommand.Rotate:
                    break;

                case TarretCommand.Break:
                    break;

                default:
                    break;
            }
        }
    }
}