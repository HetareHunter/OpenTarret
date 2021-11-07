﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Tarret
{
    public enum TarretState
    {
        Idle,
        Attack,
        Rotate,
        Break,
    }

    /// <summary>
    /// タレットのステートを管理するクラス
    /// </summary>
    public class TarretStateManager : MonoBehaviour, ITarretState
    {
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable leftHandle;
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable rightHandle;

        TarretAttacker tarretAttacker;
        AnglePointer anglePoint;
        TarretVitalManager tarretVitalManager;

        [SerializeField] GameObject tarretAnglePoint;
        bool anglePointPlayOneShot = false;

        public TarretState tarretCommandState = TarretState.Idle;

        private void Start()
        {
            tarretAttacker = GetComponent<TarretAttacker>();
            if (tarretAnglePoint != null)
            {
                anglePoint = tarretAnglePoint.GetComponent<AnglePointer>();
            }
            
            if(GetComponent<TarretVitalManager>())
            {
                tarretVitalManager = GetComponent<TarretVitalManager>();
            }
        }

        /// <summary>
        /// タレットが回転するかどうかを判定する
        /// </summary>
        public void JudgeRotateTarret()
        {
            //両手ともタレットのハンドルを握っているとき
            if (leftHandle.IsGrabbed && rightHandle.IsGrabbed)
            {
                ChangeTarretState(TarretState.Rotate);
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
        public void ChangeTarretState(TarretState next)
        {
            //以前の状態を保持
            //var prev = tarretCommandState;
            //次の状態に変更する
            tarretCommandState = next;
            //// ログを出す
            //Debug.Log($"ChangeState {prev} -> {next}");

            switch (tarretCommandState)
            {
                case TarretState.Idle:
                    break;
                case TarretState.Attack:
                    if (tarretAttacker.attackable)
                    {
                        tarretAttacker.BeginAttack();
                        leftHandle.AttackVibe();
                        rightHandle.AttackVibe();
                    }
                    ChangeTarretState(TarretState.Idle);
                    break;

                case TarretState.Rotate:
                    break;

                case TarretState.Break:
                    tarretVitalManager.TarretDeath();
                    break;

                default:
                    break;
            }
        }
    }
}