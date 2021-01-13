using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Managers
{

    /// <summary>
    /// タレットの機能ロジックを格納しているクラス
    /// </summary>
    public class BaseTarretFunction : MonoBehaviour
    {
        [SerializeField] float m_rotateSpeed = 100.0f;


        public float SetHorizontalRotateSpeed(Transform leftController, Transform rightController)
        {
            float rotateSpeed = leftController.transform.localRotation.x * rightController.localRotation.x * m_rotateSpeed;
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
            float rotateSpeed = leftController.transform.localRotation.x * rightController.localRotation.x * m_rotateSpeed;
            if (leftController.transform.localRotation.x > 0)
            {
                return -rotateSpeed;
            }
            else
            {
                return rotateSpeed;
            }
        }
    }
}