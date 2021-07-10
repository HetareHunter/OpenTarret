﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarret
{
    /// <summary>
    /// タレットの仰角、底の回転を処理するクラス
    /// </summary>
    public class TarretRotater : MonoBehaviour
    {
        /// <summary>
        /// タレットの根本部分。ここを中心に横回転をする
        /// </summary>
        [SerializeField] GameObject rootPos;
        /// <summary>
        /// タレットの縦回転をする関節
        /// </summary>
        [SerializeField] GameObject muzzleFlameJointPos;
        [SerializeField] GameObject m_leftHandlePos;
        [SerializeField] GameObject m_rightHandlePos;
        /// <summary>
        /// タレットの向きを決定する矢印
        /// </summary>
        [SerializeField] GameObject m_arrowMark;

        [SerializeField] float maxVerticalAngle = 0.3f;
        [SerializeField] float minVerticalAngle = -0.2f;

        [SerializeField] float maxHorizontalAngle = 0.5f;
        [SerializeField] float minHorizontalAngle = -0.5f;

        TarretStateManager baseTarretBrain;

        [SerializeField] float debugHorizontalRotate = 0.8f;
        [SerializeField] float debugVerticalRotate = 0.3f;

        [SerializeField] float rotateSpeed = 1.0f;

        [SerializeField] bool editRotateMode = false;

        private void Start()
        {
            baseTarretBrain = GetComponent<TarretStateManager>();
        }

        void FixedUpdate()
        {
            MoveManager();
        }

        /// <summary>
        /// tarretの動きを実行する命令を飛ばす関数
        /// </summary>
        void MoveManager()
        {
            switch (TarretStateManager.tarretCommandState)
            {
                case TarretCommand.Idle:
                    baseTarretBrain.JudgeRotateTarret();
                    break;

                case TarretCommand.Attack:
                    break;

                case TarretCommand.Rotate:
                    HorizontalRotate();
                    VerticalRotate();
                    break;

                case TarretCommand.Break:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 横回転を制御する関数
        /// </summary>
        void HorizontalRotate()
        {
            if (rootPos.transform.localRotation.y > maxHorizontalAngle)
            {
                if (m_arrowMark.transform.localRotation.y < 0)
                {
                    rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.y);
                }
            }
            else if(rootPos.transform.localRotation.y < minHorizontalAngle)
            {
                if (m_arrowMark.transform.localRotation.y > 0)
                {
                    rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.y);
                }
            }
            else
            {
                rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.y);
            }
            //rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime * m_arrowMark.transform.localRotation.y);
        }

        /// <summary>
        /// 縦回転を制御する関数
        /// </summary>
        void VerticalRotate()
        {
            //Debug.Log("muzzleFlameJointPos localRotation.x " + muzzleFlameJointPos.transform.localRotation.x);
            if (muzzleFlameJointPos.transform.localRotation.x > maxVerticalAngle)
            {
                if (m_arrowMark.transform.localRotation.x < 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
                }
            }
            else if (muzzleFlameJointPos.transform.localRotation.x < minVerticalAngle)
            {
                if (m_arrowMark.transform.localRotation.x > 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
                }
            }
            else
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
            }
        }
        #region
#if UNITY_EDITOR
        void Update()
        {
            if (editRotateMode)
            {
                float dx = Input.GetAxis("Horizontal");
                float dy = Input.GetAxis("Vertical");

                DebugHorizontalRotate(dx);
                DebugVerticalRotate(dy);

            }

        }

        void DebugHorizontalRotate(float dx)
        {
            rootPos.transform.Rotate(new Vector3(0, 90, 0) * dx * debugHorizontalRotate * Time.deltaTime);
        }

        void DebugVerticalRotate(float dy)
        {
            if (muzzleFlameJointPos.transform.localRotation.x > maxVerticalAngle)
            {
                if (dy < 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
                }
            }
            else if (muzzleFlameJointPos.transform.localRotation.x < minVerticalAngle)
            {
                if (dy > 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
                }
            }
            else
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
            }
        }
#endif
        #endregion
    }

}