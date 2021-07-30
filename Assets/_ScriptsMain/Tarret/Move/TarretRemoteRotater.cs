using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarret
{
    public class TarretRemoteRotater : MonoBehaviour
    {
        /// <summary>
        /// タレットの根本部分。ここを中心に横回転をする
        /// </summary>
        [SerializeField] GameObject rootPos;
        /// <summary>
        /// タレットの縦回転をする関節
        /// </summary>
        [SerializeField] GameObject muzzleFlameJointPos;
        /// <summary>
        /// タレットの向きを決定する矢印
        /// </summary>
        [SerializeField] GameObject m_arrowMark;

        [SerializeField] float maxVerticalAngle = 0.3f;
        [SerializeField] float minVerticalAngle = -0.2f;

        [SerializeField] float maxHorizontalAngle = 0.5f;
        [SerializeField] float minHorizontalAngle = -0.5f;

        TarretStateManager tarretStateManager;

        [SerializeField] float rotateSpeed = 0.13f;

        [SerializeField] GameObject RayPointer;
        PointerRayCaster pointerRayCaster;
        float h_angle;
        float v_angle;

        private void Start()
        {
            tarretStateManager = GetComponent<TarretStateManager>();
            pointerRayCaster = RayPointer.GetComponent<PointerRayCaster>();
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
            switch (tarretStateManager.tarretCommandState)
            {
                case TarretState.Idle:
                    tarretStateManager.JudgeRotateTarret();
                    break;

                case TarretState.Attack:
                    break;

                case TarretState.Rotate:
                    HorizontalRotate();
                    VerticalRotate();
                    break;

                case TarretState.Break:
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
            Vector3 direction = pointerRayCaster.hitPointerObj - rootPos.transform.position;
            Vector3 projectVector = Vector3.ProjectOnPlane(direction, Vector3.up);
            h_angle = Vector3.SignedAngle(Vector3.forward, projectVector, Vector3.up);
            Quaternion targetRotation = Quaternion.Euler(0, h_angle, 0);

            float rootLocalRotation_y = rootPos.transform.localRotation.y;
            if (rootLocalRotation_y > maxHorizontalAngle || rootLocalRotation_y < minHorizontalAngle)
            {
                if (targetRotation.y < maxHorizontalAngle && targetRotation.y > minHorizontalAngle)
                {
                    HRotate(targetRotation);
                }
            }
            else
            {
                HRotate(targetRotation);
            }
        }

        void HRotate(Quaternion targetRotation)
        {
            rootPos.transform.rotation = Quaternion.Slerp(rootPos.transform.rotation, targetRotation, rotateSpeed);
        }

        /// <summary>
        /// 縦回転を制御する関数見ようとするオブジェクトとmuzzleFlameとの角度を求めてから、それをQuetenion.AngleAxisで、
        /// 決めた角度のみの回転をさせる
        /// </summary>
        void VerticalRotate()
        {
            Vector3 direction = pointerRayCaster.hitPointerObj - muzzleFlameJointPos.transform.position;
            Vector3 projectVector = Vector3.ProjectOnPlane(direction, Vector3.right);
            v_angle = Vector3.SignedAngle(Vector3.forward, projectVector, Vector3.right);
            Quaternion targetRotation = Quaternion.Euler(v_angle, 0, 0);

            float muzzleFlameJointLocalRotation_x = muzzleFlameJointPos.transform.localRotation.x;

            if (muzzleFlameJointLocalRotation_x > maxVerticalAngle)
            {
                if (targetRotation.x < maxVerticalAngle)
                {
                    VRotate(targetRotation);
                }
            }
            else if (muzzleFlameJointLocalRotation_x < minVerticalAngle)
            {
                if (targetRotation.x > minVerticalAngle)
                {
                    VRotate(targetRotation);
                }
            }
            else
            {
                VRotate(targetRotation);
            }
        }

        void VRotate(Quaternion targetRotation)
        {
            muzzleFlameJointPos.transform.localRotation =
                Quaternion.Slerp(muzzleFlameJointPos.transform.localRotation, targetRotation, rotateSpeed);
        }
    }
}