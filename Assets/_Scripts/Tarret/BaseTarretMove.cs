using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    /// <summary>
    /// 実際にタレットを動かすクラス
    /// </summary>
    public class BaseTarretMove : MonoBehaviour
    {
        [SerializeField] GameObject rootPos;
        [SerializeField] GameObject muzzleFlameJointPos;
        [SerializeField] GameObject m_leftHandlePos;
        [SerializeField] GameObject m_rightHandlePos;
        [SerializeField] GameObject m_arrowMark;

        [SerializeField] float maxMuzzleFlameJointRotate = 0.3f;
        [SerializeField] float minMuzzleFlameJointRotate = -0.2f;

        //BaseTarretRotateFunction baseTarretControl;
        BaseTarretBrain baseTarretBrain;

        [SerializeField] float debugHorizontalRotate = 0.8f;
        [SerializeField] float debugVerticalRotate = 0.3f;

        private void Start()
        {
            //baseTarretControl = GetComponent<BaseTarretRotateFunction>();
            baseTarretBrain = GetComponent<BaseTarretBrain>();
        }

        void FixedUpdate()
        {
            MoveManager();
        }

        void MoveManager()
        {
            switch (baseTarretBrain.tarretCommandState)
            {
                case TarretCommand.Idle:
                    //baseTarretBrain.OldJudgeRotateTarret();

                    baseTarretBrain.JudgeRotateTarret();
                    break;
                case TarretCommand.HorizontalRotate:
                    HorizontalRotate();
                    //baseTarretBrain.OldJudgeRotateTarret();

                    baseTarretBrain.JudgeRotateTarret();
                    break;
                case TarretCommand.VerticalRotate:
                    VerticalRotate();
                    //baseTarretBrain.OldJudgeRotateTarret();

                    baseTarretBrain.JudgeRotateTarret();
                    break;
                case TarretCommand.Attack:
                    break;

                case TarretCommand.Rotate:
                    HorizontalRotate();
                    VerticalRotate();
                    break;
                default:
                    break;
            }
        }

        void HorizontalRotate()
        {
            //rootPos.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * baseTarretControl
            //        .SetHorizontalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            rootPos.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * m_arrowMark.transform.localRotation.y);
        }

        void VerticalRotate()
        {
            //Debug.Log("muzzleFlameJointPos localRotation.x " + muzzleFlameJointPos.transform.localRotation.x);
            if (muzzleFlameJointPos.transform.localRotation.x > maxMuzzleFlameJointRotate)
            {
                if (m_arrowMark.transform.localRotation.x < 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
                }
                
                //Debug.Log("leftHandle.HandleRotatePer:" + baseTarretBrain.leftHandle.HandleRotatePer);
                //Debug.Log("rightHandle.HandleRotatePer:" + baseTarretBrain.rightHandle.HandleRotatePer);
                //if (baseTarretBrain.leftHandle.HandleRotatePer < 0.0f) //回転の限界値を超えるときは逆回転の操作しか受け付けない
                //{
                //    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                //.SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));

                //}

            }
            else if (muzzleFlameJointPos.transform.localRotation.x < minMuzzleFlameJointRotate)
            {
                if (m_arrowMark.transform.localRotation.x > 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
                }
                //if (baseTarretBrain.leftHandle.HandleRotatePer > 0.0f)
                //{
                //    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                //    .SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
                //}
            }
            else
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime
                        * m_arrowMark.transform.localRotation.x);
                //muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                //    .SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            }
        }
        #region
#if UNITY_EDITOR
        void Update()
        {
            float dx = Input.GetAxis("Horizontal");
            float dy = Input.GetAxis("Vertical");

            DebugHorizontalRotate(dx);
            DebugVerticalRotate(dy);

        }

        void DebugHorizontalRotate(float dx)
        {
            rootPos.transform.Rotate(new Vector3(0, 90, 0) * dx * Time.deltaTime);
        }

        void DebugVerticalRotate(float dy)
        {
            if (muzzleFlameJointPos.transform.localRotation.x > maxMuzzleFlameJointRotate)
            {
                if (dy < 0)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
                }
            }
            else if (muzzleFlameJointPos.transform.localRotation.x < minMuzzleFlameJointRotate)
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