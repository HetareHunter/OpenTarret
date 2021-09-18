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
        [SerializeField] Color startColor;
        Color vanishingColor = new Color(0, 0, 0, 0);
        [SerializeField] Color selectedColor;
        bool _isTouch = false;
        HandlePositionResetter returnPosition;
        AnglePointer anglePointer;

        [SerializeField] GameObject handleObj;
        Renderer handleRenderer;

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

            handleRenderer = handleObj.GetComponent<Renderer>();
            //startColor = outlineRenderer.outlineMaterial.GetColor("_OutlineColor");

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
            if (isGrabbed) //握っている間の処理
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, currentController) && handle == HandleSide.Right)
                {
                    handleInput.Attack();
                }

                if (handle == HandleSide.Left)
                {
                    handleInput.CartMove(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick));
                }

                
                handFixer.FixHand(currentController); //手のメッシュの位置をハンドルの位置に固定し続けている

                if (!handleGrabMoment)//手を握った瞬間の処理
                {
                    VanishHandleOutline();

                    m_allowOffhandGrab = false;

                    handleGrabMoment = true;
                }
                
            }
            else //離している間の処理
            {
                returnPosition.Released();
                if (handleGrabMoment)//手を離した瞬間の処理
                {
                    ChangeHandleOutlineColor(_isTouch);
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
            _isTouch = true;
            if (!isGrabbed) //握ってはいないがハンドルに触れているとき
            {
                if (other.tag == "LHand")
                {
                    /*握ったときにcurrentControllerにどちらのコントローラかの情報が入るので、触れたときの振動処理は
                    currentControllerを引数に使えない*/
                    handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
                }
                else if (other.tag == "RHand")
                {
                    handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
                }
                ChangeHandleOutlineColor(_isTouch);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isTouch = false;
            if (!isGrabbed) //握っていないときでコライダーから手が離れた時
            {
                ChangeHandleOutlineColor(_isTouch);
            }
            
        }

        public void AttackVibe()
        {
            if (handleVibe != null && currentController != OVRInput.Controller.None)
            {
                handleVibe.Vibrate(tarretData.attackVibeDuration, tarretData.attackVibeFrequency,
                    tarretData.attackVibeAmplitude, currentController);
            }
        }

        /// <summary>
        /// 手がハンドルに触れただけで、握ってはいないときに色を変える
        /// </summary>
        /// <param name="isTouch">触れているかどうか</param>
        void ChangeHandleOutlineColor(bool isTouch)
        {
            if (isTouch)
            {
                handleRenderer.materials[2].SetColor("_OutlineColor", selectedColor);
            }
            else
            {
                handleRenderer.materials[2].SetColor("_OutlineColor", startColor);
            }
        }

        /// <summary>
        /// ハンドルを握ったときに、ハンドルのアウトラインを透過する
        /// </summary>
        void VanishHandleOutline()
        {
            handleRenderer.materials[2].SetColor("_OutlineColor", vanishingColor);
        }
    }
}
