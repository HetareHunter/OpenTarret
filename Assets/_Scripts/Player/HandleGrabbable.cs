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
        [SerializeField] TarretData tarretData;
        OVRInput.Controller currentController;

        public float handleRotateLimit = 20.0f;
        float m_preHandleToPlayerDis;
        float rotateAngle;

        [SerializeField] Transform m_handle;
        /// <summary> ハンドルの感度 </summary>
        [SerializeField] float handleSensitivity = 2.0f;

        [SerializeField] GameObject player;
        /// <summary>
        /// これ以上離れると自動的に手を放す
        /// </summary>
        [SerializeField] float playersArmLength = 0.7f;

        ReturnPosition returnPosition = new ReturnPosition();
        [SerializeField] GameObject tarret;
        BaseTarretBrain baseTarretBrain;

        /// <summary>
        /// 触れた時の振動の大きさ
        /// </summary>
        [SerializeField] float touchFrequeency = 0.3f;
        [SerializeField] float touchAmplitude = 0.3f;
        [SerializeField] float touchVibeDuration = 0.2f;

        protected override void Start()
        {
            returnPosition = GetComponent<ReturnPosition>();
            baseTarretBrain = tarret.GetComponent<BaseTarretBrain>();
        }
        void FixedUpdate()
        {
            if (isGrabbed)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController))
                {
                    if (currentController == OVRInput.Controller.RTouch)
                    {
                        baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
                    }
                }

                if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, currentController))
                {
                    if (IsGrabbable())
                    {
                        RotateHandle();
                    }
                    else
                    {
                        ResetRotateHandle();
                        m_preHandleToPlayerDis = 0;
                        m_allowOffhandGrab = false;
                    }
                }

                if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, currentController))
                {
                    ResetRotateHandle();
                    m_preHandleToPlayerDis = 0;
                    m_allowOffhandGrab = true;
                    currentController = OVRInput.Controller.None;
                    baseTarretBrain.ChangeTarretState(TarretCommand.Idle);
                }
            }
            else
            {
                returnPosition.Released();
                m_preHandleToPlayerDis = 0;
                m_allowOffhandGrab = true;
                currentController = OVRInput.Controller.None;
            }

            

        }

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "LHand" && !isGrabbed)
            {
                VibrationExtension.Instance.VibrateController(
                    touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
            }
            else if (other.tag == "RHand" && !isGrabbed)
            {
                VibrationExtension.Instance.VibrateController(
                    touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
            }
        }

        public void AttackVibe()
        {
            VibrationExtension.Instance.VibrateController(
                        tarretData.attackVibeDuration, tarretData.attackVibeFrequency, tarretData.attackVibeAmplitude, currentController);
        }
    }
}