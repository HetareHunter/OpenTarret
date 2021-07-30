using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarret
{
    public class TarretRemoteRotater : MonoBehaviour
    {
        /// <summary>
        /// �^���b�g�̍��{�����B�����𒆐S�ɉ���]������
        /// </summary>
        [SerializeField] GameObject rootPos;
        /// <summary>
        /// �^���b�g�̏c��]������֐�
        /// </summary>
        [SerializeField] GameObject muzzleFlameJointPos;
        /// <summary>
        /// �^���b�g�̌��������肷����
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
        /// tarret�̓��������s���閽�߂��΂��֐�
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
        /// ����]�𐧌䂷��֐�
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
        /// �c��]�𐧌䂷��֐����悤�Ƃ���I�u�W�F�N�g��muzzleFlame�Ƃ̊p�x�����߂Ă���A�����Quetenion.AngleAxis�ŁA
        /// ���߂��p�x�݂̂̉�]��������
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