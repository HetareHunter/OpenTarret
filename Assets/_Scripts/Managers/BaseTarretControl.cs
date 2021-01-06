using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// タレットの機能ロジックを格納しているクラス
    /// </summary>
    public class BaseTarretControl : MonoBehaviour
    {
        [SerializeField] float m_rotateSpeed = 100.0f;

        public float SetRotateSpeed(Transform leftController,Transform rightController)
        {
            float rotateSpeed = leftController.transform.localRotation.x * rightController.localRotation.x * m_rotateSpeed;
            return rotateSpeed;
        }

    }
}