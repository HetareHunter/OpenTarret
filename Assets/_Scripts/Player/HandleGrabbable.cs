using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    /// <summary>
    /// 実際にハンドルを握ることができるコライダー部分に付ける
    /// </summary>
    public class HandleGrabbable : OVRGrabbable, IGrabbable
    {
        OVRInput.Controller currentController;

        ReturnPosition returnPosition = new ReturnPosition();
        [SerializeField] Transform m_handle;
        public float handleRotateLimit = 20.0f;
        float m_preHandleToPlayerDis;
        float rotateAngle;
        /// <summary> ハンドルの感度 </summary>
        [SerializeField] float handleSensitivity = 2.0f;

        [SerializeField] GameObject player;
        /// <summary>
        /// これ以上離れると自動的に手を放す
        /// </summary>
        [SerializeField] float playersArmLength = 0.7f;

        [SerializeField] BaseTarretBrain baseTarretBrain;

        /// <summary>
        /// ハンドルがどれほど回転した状態になっているかの割合
        /// </summary>
        public float HandleRotatePer
        {
            get
            {
                if (m_handle.transform.localEulerAngles.x >= 180)
                {
                    return (m_handle.transform.localEulerAngles.x - 360) / handleRotateLimit;
                }
                else
                {
                    return m_handle.transform.localEulerAngles.x / handleRotateLimit;
                }
            }
        }

        public void GrabBegin(OVRInput.Controller controller)
        {
            currentController = controller;
        }


        protected override void Start()
        {
            returnPosition = GetComponent<ReturnPosition>();
        }
        void FixedUpdate()
        {
            if (!isGrabbed)
            {
                returnPosition.Released();
                ResetRotateHandle();
                m_preHandleToPlayerDis = 0;
                m_allowOffhandGrab = true;
            }

            if (isGrabbed && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController))
            {
                // implement
                if (currentController == OVRInput.Controller.RTouch)
                {
                    baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
                }
            }


            if (isGrabbed && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, currentController))
            {
                if (IsGrabbable())
                {
                    RotateHandle();
                }
                else
                {
                    returnPosition.Released();
                    ResetRotateHandle();
                    m_preHandleToPlayerDis = 0;
                    m_allowOffhandGrab = false;
                }

            }

        }

        void RotateHandle()
        {
            float handleMoveDistance = MeasurementGrabToPlayer() / Time.deltaTime * handleSensitivity;
            //Debug.Log("handleMoveDistance : " + handleMoveDistance);
            rotateAngle += handleMoveDistance;
            rotateAngle = Mathf.Clamp(rotateAngle, -handleRotateLimit, handleRotateLimit);
            m_handle.localRotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
        }

        /// <summary>
        /// コライダーのプレイヤーとの距離に応じてハンドルが回るようにする
        /// </summary>
        float MeasurementGrabToPlayer()
        {
            float handleToPlayerDis = Vector3.Distance(transform.position, player.transform.position);
            float preHandleToPlayerDis = m_preHandleToPlayerDis;
            m_preHandleToPlayerDis = handleToPlayerDis;

            return preHandleToPlayerDis - handleToPlayerDis;
        }

        /// <summary>
        /// プレイヤーはハンドルをつかめるかどうかの判定
        /// 毎フレーム判定し、コライダーがタレットのハンドルから一定の距離以上離れたら手を放す
        /// </summary>
        /// <returns></returns>
        bool IsGrabbable()
        {
            return Vector3.Distance(transform.position, player.transform.position) < playersArmLength;
        }

        void ResetRotateHandle()
        {
            rotateAngle = 0;
            //m_handle.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
            m_handle.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}