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

        [SerializeField] float maxMuzzleFlameJointRotate = 120.0f;
        [SerializeField] float minMuzzleFlameJointRotate = 50.0f;

        BaseTarretFunction baseTarretControl;
        BaseTarretBrain baseTarretBrain;

        private void Start()
        {
            baseTarretControl = GetComponent<BaseTarretFunction>();
            baseTarretBrain = GetComponent<BaseTarretBrain>();
        }

        void FixedUpdate()
        {
            baseTarretBrain.JudgeTarretCommandState();
            if (baseTarretBrain.tarretCommanfState == TarretCommand.HorizontalRotate)
            {
                HorizontalRotate();
            }
            else if (baseTarretBrain.tarretCommanfState == TarretCommand.VerticalRotate)
            {
                VerticalRotate();
            }
            else //baseTarretControl.tarretCommanfStateがIdleのステートの場合
            {

            }

        }

        void HorizontalRotate()
        {
            rootPos.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * baseTarretControl
                    .SetHorizontalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
        }

        void VerticalRotate()
        {
            if (muzzleFlameJointPos.transform.localEulerAngles.x > maxMuzzleFlameJointRotate)
            {
                if (baseTarretBrain.leftHandle.HandleRotatePer > 0.0f) //回転の限界値を超えるときは逆回転の操作しか受け付けない
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                .SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
                }
                
            }
            else if(muzzleFlameJointPos.transform.localEulerAngles.x < minMuzzleFlameJointRotate)
            {
                if(baseTarretBrain.leftHandle.HandleRotatePer < 0.0f)
                {
                    muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                    .SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
                }
            }
            else
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * baseTarretControl
                    .SetVerticalRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            }
        }

        void LimitVerticalRotate()
        {

        }
    }
}