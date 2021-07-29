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
        //[SerializeField] GameObject m_leftHandlePos;
        //[SerializeField] GameObject m_rightHandlePos;
        /// <summary>
        /// �^���b�g�̌��������肷����
        /// </summary>
        [SerializeField] GameObject m_arrowMark;

        [SerializeField] float maxVerticalAngle = 0.3f;
        [SerializeField] float minVerticalAngle = -0.2f;

        [SerializeField] float maxHorizontalAngle = 0.5f;
        [SerializeField] float minHorizontalAngle = -0.5f;

        TarretStateManager tarretStateManager;

        //[SerializeField] float debugHorizontalRotate = 0.8f;
        //[SerializeField] float debugVerticalRotate = 0.3f;

        [SerializeField] float rotateSpeed = 1.0f;

        [SerializeField] GameObject RayPointer;
        PointerRayCaster pointerRayCaster;
        Vector3 h_direction;
        Vector3 v_direction;
        float h_angle;
        float v_angle;
        Quaternion horizontalAngle;
        Quaternion verticalAngle;

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


            //h_direction = pointerRayCaster.hitPointerObj - rootPos.transform.position;
            //h_direction.y = 0;
            //Quaternion targetRotation = Quaternion.LookRotation(h_direction);
            if (rootPos.transform.localRotation.y > maxHorizontalAngle)
            {
                if (targetRotation.y < maxHorizontalAngle)
                {
                    rootPos.transform.rotation = Quaternion.Slerp(rootPos.transform.rotation, targetRotation, rotateSpeed);
                }
            }
            else if (rootPos.transform.localRotation.y < minHorizontalAngle)
            {
                if (targetRotation.y > minHorizontalAngle)
                {
                    rootPos.transform.rotation = Quaternion.Slerp(rootPos.transform.rotation, targetRotation, rotateSpeed);
                }
            }
            else
            {
                rootPos.transform.rotation = Quaternion.Slerp(rootPos.transform.rotation, targetRotation, rotateSpeed);
            }
        }

        //void HRotate()
        //{
        //    h_relativePos = pointerRayCaster.hitPointerObj - rootPos.transform.position;
        //    h_relativePos.y=0;
        //    Quaternion targetRotation = Quaternion.LookRotation(h_relativePos);
        //    rootPos.transform.rotation = Quaternion.Slerp(rootPos.transform.rotation, targetRotation, rotateSpeed);

        //    //rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
        //    //                        * m_arrowMark.transform.rotation.y);
        //}

        /// <summary>
        /// �c��]�𐧌䂷��֐����悤�Ƃ���I�u�W�F�N�g��muzzleFlame�Ƃ̊p�x�����߂Ă���A�����Quetenion.AngleAxis�ŁA
        /// ���߂��p�x�݂̂̉�]��������
        /// </summary>
        void VerticalRotate()
        {
            Vector3 direction = pointerRayCaster.hitPointerObj - muzzleFlameJointPos.transform.position;
            Vector3 projectVector = Vector3.ProjectOnPlane(direction, Vector3.right);
            v_angle = Vector3.SignedAngle(Vector3.forward, projectVector, Vector3.right);
            Quaternion targetRotation = Quaternion.Euler(v_angle, h_angle, 0);
            //Debug.Log("muzzleFlameJointPos localRotation.x " + muzzleFlameJointPos.transform.localRotation.x);
            //verticalAngle =
            //    Quaternion.Slerp(muzzleFlameJointPos.transform.rotation, targetRotation, rotateSpeed);
            if (muzzleFlameJointPos.transform.localRotation.x > maxVerticalAngle)
            {
                if (targetRotation.x < maxVerticalAngle)
                {
                    verticalAngle =
                Quaternion.Slerp(muzzleFlameJointPos.transform.rotation, targetRotation, rotateSpeed);
                }
            }
            else if (muzzleFlameJointPos.transform.localRotation.x < minVerticalAngle)
            {
                if (targetRotation.x > minVerticalAngle)
                {
                    verticalAngle =
                Quaternion.Slerp(muzzleFlameJointPos.transform.rotation, targetRotation, rotateSpeed);
                }
            }
            else
            {
                verticalAngle =
                Quaternion.Slerp(muzzleFlameJointPos.transform.rotation, targetRotation, rotateSpeed);
            }

            
            muzzleFlameJointPos.transform.rotation = verticalAngle;
        }

        void VRotate()
        {
            v_direction = pointerRayCaster.hitPointerObj - muzzleFlameJointPos.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(v_direction);
            muzzleFlameJointPos.transform.rotation =
                Quaternion.Slerp(muzzleFlameJointPos.transform.rotation, targetRotation, rotateSpeed);

            //muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
            //                        * m_arrowMark.transform.rotation.x);
        }
    }
}