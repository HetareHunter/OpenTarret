using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Tarret
{
    public enum TarretStateType
    {
        Idle,
        Attack,
        Rotate,
        Break,
    }

    /// <summary>
    /// タレットのステートを管理するクラス
    /// </summary>
    public class TarretStateManager : MonoBehaviour, ITarretStateChangeable
    {
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable _leftHandle;
        ///<summary>Tarretのhandleを握ったときに情報が格納される変数</summary>
        public HandleGrabbable _rightHandle;

        TarretAttacker _tarretAttacker;
        AnglePointer _anglePoint;
        TarretVitalManager _tarretVitalManager;

        [SerializeField] GameObject tarretAnglePoint;
        bool anglePointPlayOneShot = false;

        TarretStateType _tarretStateType = TarretStateType.Idle;
        ITarretStateEnterble _tarretStateEnterble;
        /// <summary>gameStaterのインスタンスのキャッシュ </summary>
        Dictionary<TarretStateType, ITarretStateEnterble> _tarretStateTypes = new Dictionary<TarretStateType, ITarretStateEnterble>();

        private void Start()
        {
            _tarretAttacker = GetComponent<TarretAttacker>();
            if (tarretAnglePoint != null)
            {
                _anglePoint = tarretAnglePoint.GetComponent<AnglePointer>();
            }

            if (GetComponent<TarretVitalManager>())
            {
                _tarretVitalManager = GetComponent<TarretVitalManager>();
            }

            _tarretStateTypes.Add(TarretStateType.Idle, new TarretIdle());
            _tarretStateTypes.Add(TarretStateType.Rotate, new TarretRotate());
            _tarretStateTypes.Add(TarretStateType.Attack, new TarretAttack(_tarretAttacker, _leftHandle, _rightHandle));
            _tarretStateTypes.Add(TarretStateType.Break, new TarretBreak(_tarretVitalManager));
        }

        /// <summary>
        /// タレットが回転するかどうかを判定する
        /// </summary>
        public void JudgeRotateTarret()
        {
            //両手ともタレットのハンドルを握っているとき
            if (_leftHandle.IsGrabbed && _rightHandle.IsGrabbed)
            {
                //ChangeTarretState(TarretStateType.Rotate);
                ToRotate();
                if (anglePointPlayOneShot)
                {
                    _anglePoint.BeginGrabHandle();
                    anglePointPlayOneShot = false;
                }
            }
            else
            {
                anglePointPlayOneShot = true;
            }
        }

        /// <summary>
        /// Tarretのステート変化を行う関数をここにまとめている
        /// </summary>
        /// <param name="next"></param>
        public void ChangeTarretState(TarretStateType next)
        {
            //以前の状態を保持
            //var prev = tarretCommandState;
            //次の状態に変更する
            _tarretStateType = next;
            //// ログを出す
            //Debug.Log($"ChangeState {prev} -> {next}");

            switch (_tarretStateType)
            {
                case TarretStateType.Idle:
                    break;
                case TarretStateType.Attack:
                    if (_tarretAttacker.attackable)
                    {
                        _tarretAttacker.BeginAttack();
                        _leftHandle.AttackVibe();
                        _rightHandle.AttackVibe();
                    }
                    ChangeTarretState(TarretStateType.Idle);
                    break;

                case TarretStateType.Rotate:
                    break;

                case TarretStateType.Break:
                    _tarretVitalManager.TarretDeath();
                    break;

                default:
                    break;
            }
        }

        public void ToIdle()
        {
            _tarretStateType = TarretStateType.Idle;
            _tarretStateEnterble = _tarretStateTypes[_tarretStateType];
            _tarretStateEnterble.EnterTarretState();
        }

        public void ToRotate()
        {
            _tarretStateType = TarretStateType.Rotate;
            _tarretStateEnterble = _tarretStateTypes[_tarretStateType];
            _tarretStateEnterble.EnterTarretState();
        }

        public void ToAttack()
        {
            _tarretStateType = TarretStateType.Attack;
            _tarretStateEnterble = _tarretStateTypes[_tarretStateType];
            _tarretStateEnterble.EnterTarretState();

            ToIdle();
        }

        public void ToBreak()
        {
            _tarretStateType = TarretStateType.Break;
            _tarretStateEnterble = _tarretStateTypes[_tarretStateType];
            _tarretStateEnterble.EnterTarretState();
        }

        public TarretStateType GetTarretState()
        {
            return _tarretStateType;
        }
    }

    public class TarretIdle : ITarretStateEnterble
    {
        public TarretIdle()
        {

        }

        public void EnterTarretState()
        {

        }
    }

    public class TarretRotate : ITarretStateEnterble
    {
        public TarretRotate()
        {

        }

        public void EnterTarretState()
        {

        }
    }

    public class TarretAttack : ITarretStateEnterble
    {
        TarretAttacker _tarretAttacker;
        HandleGrabbable _leftHandle;
        HandleGrabbable _rightHandle;
        public TarretAttack(TarretAttacker tarretAttacker, HandleGrabbable leftHandleGrabbable, HandleGrabbable rightHandleGrabbable)
        {
            _tarretAttacker = tarretAttacker;
            _leftHandle = leftHandleGrabbable;
            _rightHandle = rightHandleGrabbable;
        }

        public void EnterTarretState()
        {
            if (_tarretAttacker.attackable)
            {
                _tarretAttacker.BeginAttack();
                _leftHandle.AttackVibe();
                _rightHandle.AttackVibe();
            }
        }
    }

    public class TarretBreak : ITarretStateEnterble
    {
        TarretVitalManager _tarretVitalManager;
        public TarretBreak(TarretVitalManager tarretVitalManager)
        {
            _tarretVitalManager = tarretVitalManager;
        }

        public void EnterTarretState()
        {
            _tarretVitalManager.TarretDeath();
        }
    }
}