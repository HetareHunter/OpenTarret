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
        [SerializeField] GameObject RootPosi;
        [SerializeField] GameObject m_leftHandlePos;
        [SerializeField] GameObject m_rightHandlePos;

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
            //if (m_leftHandlePos.transform.localRotation.x < m_rightHandlePos.transform.localRotation.x) //左回りの回転をする
            //{
            //    RootPosi.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * -baseTarretControl
            //        .SetRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            //}
            //else if (m_leftHandlePos.transform.localRotation.x > m_rightHandlePos.transform.localRotation.x) //右回りの回転をする
            //{
            //    RootPosi.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * baseTarretControl
            //        .SetRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            //}
            RootPosi.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * baseTarretControl
                    .SetRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
        }

        void VerticalRotate()
        {

        }
    }
}