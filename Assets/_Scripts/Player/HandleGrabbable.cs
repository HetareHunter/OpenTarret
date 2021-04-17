using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    /// <summary>
    /// 実際にハンドルを握ることができるコライダー部分にコンポーネントを付ける
    /// 手についているクラスが処理を考えるのではなく握ったオブジェクトが処理を行う方向で組み立てる
    /// </summary>
    public class HandleGrabbable : OVRGrabbable, IGrabbable
    {
        [SerializeField] TarretAttackData tarretData;
        OVRInput.Controller currentController;

        public float handleRotateLimit = 20.0f;
        //float m_preHandleToPlayerDis;
        //float rotateAngle;

        [SerializeField] Transform m_handle;
        /// <summary> ハンドルの感度 </summary>
        //[SerializeField] float handleSensitivity = 2.0f;

        [SerializeField] GameObject leftHandMesh;
        [SerializeField] GameObject rightHandMesh;
        [SerializeField] GameObject gripPosi;

        [SerializeField] GameObject player;

        ReturnPosition returnPosition = new ReturnPosition();
        [SerializeField] GameObject tarret;
        BaseTarretBrain baseTarretBrain;

        /// <summary> 触れた時の振動の大きさ </summary>
        [SerializeField] float touchFrequeency = 0.3f;
        /// <summary> 触れた時の振動の周波数 </summary>
        [SerializeField] float touchAmplitude = 0.3f;
        /// <summary> 触れた時の振動の時間 </summary>
        [SerializeField] float touchVibeDuration = 0.2f;

        /// <summary>
        /// 手でつかんだ瞬間のフラグ
        /// </summary>
        bool handleGrabMoment = false;


        protected override void Start()
        {
            returnPosition = GetComponent<ReturnPosition>();
            baseTarretBrain = tarret.GetComponent<BaseTarretBrain>();
        }
        void FixedUpdate()
        {
            GrabMethod();
        }

        /// <summary>
        /// 握っているときの処理
        /// </summary>
        private void GrabMethod()
        {
            if (isGrabbed)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController))
                {
                    if (transform.tag == "RHundle")
                    {
                        baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
                    }
                }


                if (currentController == OVRInput.Controller.LTouch)
                {
                    leftHandMesh.transform.position = gripPosi.transform.position;
                }
                else if (currentController == OVRInput.Controller.RTouch)
                {
                    rightHandMesh.transform.position = gripPosi.transform.position;
                }

                handleGrabMoment = true;
            }
            else
            {
                returnPosition.Released();
                if (handleGrabMoment)
                {
                    m_allowOffhandGrab = true;
                    if (currentController == OVRInput.Controller.LTouch)
                    {
                        leftHandMesh.transform.localPosition = Vector3.zero;
                    }
                    else if (currentController == OVRInput.Controller.RTouch)
                    {
                        rightHandMesh.transform.localPosition = Vector3.zero;
                    }
                    currentController = OVRInput.Controller.None;
                    baseTarretBrain.ChangeTarretState(TarretCommand.Idle);

                    handleGrabMoment = false;
                }

            }
        }

        /// <summary>
        /// ハンドルがどれほど回転した状態になっているかの割合
        /// </summary>
        //public float HandleRotatePer
        //{
        //    get
        //    {
        //        if (m_handle.transform.localEulerAngles.x >= 180)
        //        {
        //            return (m_handle.transform.localEulerAngles.x - 360) / handleRotateLimit;
        //        }
        //        else
        //        {
        //            return m_handle.transform.localEulerAngles.x / handleRotateLimit;
        //        }
        //    }
        //}

        public void GrabBegin(OVRInput.Controller controller)
        {
            currentController = controller;
        }

        //void RotateHandle()
        //{
        //    float handleMoveDistance = MeasurementGrabToPlayer() / Time.deltaTime * handleSensitivity;
        //    //Debug.Log("handleMoveDistance : " + handleMoveDistance);
        //    rotateAngle += handleMoveDistance;
        //    rotateAngle = Mathf.Clamp(rotateAngle, -handleRotateLimit, handleRotateLimit);
        //    m_handle.localRotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
        //}

        /// <summary>
        /// コライダーのプレイヤーとの距離に応じてハンドルが回るようにする
        /// </summary>
        //float MeasurementGrabToPlayer()
        //{
        //    float handleToPlayerDis = Vector3.Distance(transform.position, player.transform.position);
        //    float preHandleToPlayerDis = m_preHandleToPlayerDis;
        //    m_preHandleToPlayerDis = handleToPlayerDis;

        //    return preHandleToPlayerDis - handleToPlayerDis;
        //}

        /// <summary>
        /// プレイヤーはハンドルをつかめるかどうかの判定
        /// 毎フレーム判定し、コライダーがタレットのハンドルから一定の距離以上離れたら手を放す
        /// </summary>
        /// <returns></returns>
        //bool IsGrabbable()
        //{
        //    return Vector3.Distance(transform.position, player.transform.position) < playersArmLength;
        //}

        //void ResetRotateHandle()
        //{
        //    rotateAngle = 0;
        //    //m_handle.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.right);
        //    m_handle.rotation = new Quaternion(0, 0, 0, 0);
        //}

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