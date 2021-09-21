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
    [RequireComponent(typeof(HandFixer))]
    [RequireComponent(typeof(HandleInput))]
    [RequireComponent(typeof(HandleVibe))]
    /// <summary>
    /// 実際にハンドルを握ることができるコライダー部分にコンポーネントを付ける
    /// 手についているクラスが処理を考えるのではなく握ったオブジェクトが処理を行う方向で組み立てる
    /// </summary>
    public class HandleGrabbable : MonoBehaviour, IGrabbable, ISelectable
    {
        [SerializeField] TarretAttackData tarretData;
        /// <summary>
        /// 握っている手が左か右かのステート
        /// </summary>
        OVRInput.Controller currentController;
        /// <summary>
        /// 握っているかどうか
        /// </summary>
        bool _isGrabbed;
        /// <summary>
        /// 握ることのできるかどうか
        /// </summary>
        public bool _allowOffhandGrab = true;

        [Inject]
        ITarretState TarretState;
        [SerializeField] GameObject anglePointerObj;
        [SerializeField] Color startColor;
        Color vanishingColor = new Color(0, 0, 0, 0);
        [SerializeField] Color selectedColor;
        /// <summary>
        /// 手のレイが当たっているかどうか
        /// </summary>
        bool _isTouch = false;
        bool _isTouchMoment = false;
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

        Transform grabbedHandTransform;

        public bool IsGrabbed
        {
            get
            {
                return _isGrabbed;
            }
            private set { }
        }


        void Start()
        {
            if (anglePointerObj != null)
            {
                anglePointer = anglePointerObj.GetComponent<AnglePointer>();
            }

            handleRenderer = handleObj.GetComponent<Renderer>();

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
        /// 掴む処理
        /// </summary>
        private void GrabMethod()
        {
            if (_isGrabbed) //握っている間の処理
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
                FollowHand(grabbedHandTransform);

                if (!handleGrabMoment)//手を握った瞬間の処理
                {
                    VanishOutline();
                    _allowOffhandGrab = false;
                    handleGrabMoment = true;
                }
            }
            else //離している間の処理
            {
                if (handleGrabMoment)//手を離した瞬間の処理
                {
                    GrabEnd();
                }
            }
        }

        /// <summary>
        /// ハンドルのコリジョン部分を手のレイで当てて、中指のトリガーを引くことで握る処理を始めるメソッド
        /// </summary>
        /// <param name="controller"></param>
        public void GrabBegin(OVRInput.Controller controller, Transform transform)
        {
            currentController = controller;
            grabbedHandTransform = transform;
            _isGrabbed = true;
        }

        /// <summary>
        /// 手の中指トリガーを離したとき1回だけ呼び出されるメソッド
        /// </summary>
        public void GrabEnd()
        {
            handFixer.ReleseHand(currentController);
            currentController = OVRInput.Controller.None;

            returnPosition.Released();
            ChangeOutlineColor(_isTouch);

            TarretState.ChangeTarretState(Tarret.TarretState.Idle);
            grabbedHandTransform = null;
            anglePointer.isAdjust = false;
            handleGrabMoment = false;
            _allowOffhandGrab = true;
            _isGrabbed = false;
        }


        //private void OnTriggerEnter(Collider other)
        //{
        //    _isTouch = true;
        //    if (!_isGrabbed) //握ってはいないがハンドルに触れているとき
        //    {
        //        if (other.tag == "LHand")
        //        {
        //            //握ったときにcurrentControllerにどちらのコントローラかの情報が入るので、触れたときの振動処理は
        //            //currentControllerを引数に使えない
        //            handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
        //        }
        //        else if (other.tag == "RHand")
        //        {
        //            handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
        //        }
        //        ChangeOutlineColor(_isTouch);
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    _isTouch = false;
        //    if (!_isGrabbed) //握っていないときでコライダーから手が離れた時
        //    {
        //        ChangeOutlineColor(_isTouch);
        //    }
        //}

        /// <summary>
        /// 手から放たれ続けているレイがオブジェクトに触れているときの処理
        /// </summary>
        /// <param name="isTouch"></param>
        /// <param name="hand"></param>
        public void OnTouch(bool isTouch, Hand hand)
        {
            _isTouch = isTouch;
            if (!_isTouchMoment && isTouch)//触れた瞬間の処理
            {
                TouchEnter(isTouch, hand);
            }
            else if (_isTouchMoment && !isTouch)
            {
                TouchExit(isTouch);
            }
        }

        /// <summary>
        /// レイがオブジェクトに触れた瞬間の処理
        /// </summary>
        /// <param name="isTouch"></param>
        /// <param name="hand"></param>
        void TouchEnter(bool isTouch, Hand hand)
        {
            _isTouchMoment = isTouch;
            if (hand == Hand.Left)
            {
                //握ったときにcurrentControllerにどちらのコントローラかの情報が入るので、触れたときの振動処理は
                //currentControllerを引数に使えない
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
            }
            else
            {
                handleVibe.Vibrate(touchVibeDuration, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
            }

            ChangeOutlineColor(_isTouch);
        }

        /// <summary>
        /// 触れたレイがオブジェクトから離れた瞬間の処理
        /// </summary>
        /// <param name="isTouch"></param>
        public void TouchExit(bool isTouch)
        {
            _isTouchMoment = isTouch;
            ChangeOutlineColor(_isTouch);
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
        public void ChangeOutlineColor(bool isTouch)
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
        public void VanishOutline()
        {
            handleRenderer.materials[2].SetColor("_OutlineColor", vanishingColor);
        }

        void FollowHand(Transform handTransform)
        {
            transform.SetPositionAndRotation(handTransform.position, handTransform.rotation);
        }
    }
}
