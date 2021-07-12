using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;
using Zenject;

public enum HandleSide
{
    Left,
    Right
}

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

        [Inject]
        ITarretState TarretState;
        [SerializeField] GameObject anglePointerObj;
        HandlePositionResetter returnPosition = new HandlePositionResetter();
        AnglePointer anglePointer;
        HandleVibe handleVibe;
        HandleInput handleInput;
        HandFixer handFixer;
        public HandleSide handle;

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
            returnPosition = GetComponent<HandlePositionResetter>();
            anglePointer = anglePointerObj.GetComponent<AnglePointer>();
            handleVibe = GetComponent<HandleVibe>();
            handleInput = GetComponent<HandleInput>();
            handFixer = GetComponent<HandFixer>();
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
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController) && handle == HandleSide.Right)
                {
                    handleInput.Attack();
                }

                if (handle == HandleSide.Left)
                {
                    handleInput.CartMove(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick));
                }

                handFixer.FixHand(currentController);
                handleGrabMoment = true;
            }
            else
            {
                returnPosition.Released();
                if (handleGrabMoment)//手を離した瞬間の処理
                {
                    m_allowOffhandGrab = true;

                    handFixer.ReleseHand(currentController);
                    currentController = OVRInput.Controller.None;

                    TarretState.ChangeTarretState(TarretCommand.Idle);

                    anglePointer.isAdjust = false;

                    handleGrabMoment = false;
                }

            }
        }


        public void GrabBegin(OVRInput.Controller controller)
        {
            currentController = controller;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "LHand" && !isGrabbed)
            {
                /*握ったときにcurrentControllerにどちらのコントローラかの情報が入るので、触れたときの振動処理は
                currentCntrollerを引数に使えない*/
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
            }
            else if (other.tag == "RHand" && !isGrabbed)
            {
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
            }
        }

        public void AttackVibe()
        {
            if (handleVibe != null)
            {
                handleVibe.Vibrate(tarretData.attackVibeDuration, tarretData.attackVibeFrequency,
                    tarretData.attackVibeAmplitude, currentController);
            }
        }
    }
}
