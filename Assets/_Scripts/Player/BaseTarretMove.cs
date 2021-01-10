using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    public class BaseTarretMove : MonoBehaviour
    {
        [SerializeField] GameObject RootPosi;
        [SerializeField] GameObject m_leftHandlePos;
        [SerializeField] GameObject m_rightHandlePos;

        BaseTarretControl baseTarretControl;
        private void Start()
        {
            baseTarretControl = GetComponent<BaseTarretControl>();
        }

        void FixedUpdate()
        {
            if (m_leftHandlePos.transform.localRotation.x < m_rightHandlePos.transform.localRotation.x) //左回りの回転をする
            {
                RootPosi.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * -baseTarretControl
                    .SetRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            }
            else if (m_leftHandlePos.transform.localRotation.x > m_rightHandlePos.transform.localRotation.x) //右回りの回転をする
            {
                RootPosi.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * baseTarretControl
                    .SetRotateSpeed(m_leftHandlePos.transform, m_rightHandlePos.transform));
            }
        }
    }
}