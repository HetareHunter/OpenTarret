using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Managers
{

    /// <summary>
    /// タレットの機能ロジックを格納しているクラス
    /// </summary>
    public class BaseTarretRotateFunction : MonoBehaviour
    {
        [SerializeField] float m_horizontalRotateSpeed = 100.0f;
        [SerializeField] float m_verticalRotateSpeed = 20.0f;


        public float SetHorizontalRotateSpeed(Transform leftController, Transform rightController)
        {
            float rotateSpeed = leftController.transform.localRotation.x * rightController.localRotation.x * m_horizontalRotateSpeed;
            if (leftController.transform.localRotation.x > rightController.localRotation.x)
            {
                return rotateSpeed;
            }
            else
            {
                return -rotateSpeed;
            }
        }

        public float SetVerticalRotateSpeed(Transform leftController, Transform rightController)
        {
            float rotateSpeed = leftController.transform.localRotation.x * rightController.localRotation.x * m_verticalRotateSpeed;
            if (leftController.transform.localRotation.x > 0)
            {
                return rotateSpeed;
            }
            else
            {
                return -rotateSpeed;
            }
        }
    }
}