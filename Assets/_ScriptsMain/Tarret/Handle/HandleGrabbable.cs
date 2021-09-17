using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;
using Zenject;
using UnityEngine.Rendering.Universal;

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
        Color startColor;
        [SerializeField] Color selectedColor;
        [SerializeField] ForwardRendererData outlineRendererData;
        OutlineRenderer outlineRenderer;
        HandlePositionResetter returnPosition;
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
            if (anglePointerObj != null)
            {
                anglePointer = anglePointerObj.GetComponent<AnglePointer>();
            }

            if (handle == HandleSide.Left)
            {
                outlineRenderer = (OutlineRenderer)outlineRendererData.rendererFeatures[0];
            }
            else
            {
                outlineRenderer = (OutlineRenderer)outlineRendererData.rendererFeatures[1];
            }

            startColor = outlineRenderer.outlineMaterial.GetColor("_OutlineColor");

            handleVibe = GetComponent<HandleVibe>();
            handleInput = GetComponent<HandleInput>();
            handFixer = GetComponent<HandFixer>();
            returnPosition = GetComponent<HandlePositionResetter>();
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

                m_allowOffhandGrab = false;
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

                    TarretState.ChangeTarretState(Tarret.TarretState.Idle);

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
                currentControllerを引数に使えない*/
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
            }
            else if (other.tag == "RHand" && !isGrabbed)
            {
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
            }

            ChangeColor(true);
        }

        private void OnTriggerExit(Collider other)
        {
            ChangeColor(false);
        }

        public void AttackVibe()
        {
            if (handleVibe != null && currentController != OVRInput.Controller.None)
            {
                handleVibe.Vibrate(tarretData.attackVibeDuration, tarretData.attackVibeFrequency,
                    tarretData.attackVibeAmplitude, currentController);
            }
        }

        void ChangeColor(bool grabbable)
        {
            if (grabbable)
            {
                outlineRenderer.outlineMaterial.SetColor("_OutlineColor", selectedColor);
            }
            else
            {
                outlineRenderer.outlineMaterial.SetColor("_OutlineColor", startColor);
            }
        }
    }
}
