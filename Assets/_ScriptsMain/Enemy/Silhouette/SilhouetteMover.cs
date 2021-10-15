using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ターゲットであるシルエットを起こしたり倒したりするクラス。DoTweenでアニメーションさせている
/// 人のシルエットはPositionが動かない
/// </summary>
public class SilhouetteMover : MonoBehaviour, IMovableSilhouette
{
    [SerializeField] float _rotateTime = 1.0f;

    /// <summary>
    /// 生きている時間
    /// </summary>
    [SerializeField] float _activeTime = 8.0f;

    SilhouetteActivatior _silhouetteActivatior;

    Tweener _startTween;
    Sequence _mySequence;

    [SerializeField] Vector3[] _wayPoint;
    [SerializeField] float _moveDuration = 2.0f;
    [SerializeField] float _standbyTime = 0.5f;
    Vector3 startPosi;

    /// <summary>
    /// 現在のスタンドの状態
    /// </summary>
    SilhouetteStandState _standState = SilhouetteStandState.Down;
    // Start is called before the first frame update
    void Start()
    {
        _silhouetteActivatior = GetComponentInChildren<SilhouetteActivatior>();
        DOTween.Init();
        startPosi = transform.position;
        SetTweenPath();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DoPlayTween();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DoPauseTween();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DoKillPause();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DoRestart();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetTweenPath();
        }
    }

    private void Reset()
    {
        transform.position = startPosi;
    }

    /// <summary>
    /// シルエットの回転軸を回転させるメソッド
    /// </summary>
    /// <param name="standState">これからこの状態にしたいというhikisuu
    /// </param>
    /// <param name="rotateTime"></param>
    public void StandSilhouette(SilhouetteStandState standState, float rotateTime)
    {
        if (_standState == SilhouetteStandState.Down && standState == SilhouetteStandState.Up)//起き上がる時の処理
        {
            transform.DORotate(new Vector3(-90, 0, 0), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        else if (_standState == SilhouetteStandState.Up && standState == SilhouetteStandState.Down)//倒れる時の処理
        {
            transform.DORotate(new Vector3(90, 0, 0), rotateTime, RotateMode.LocalAxisAdd)
                .OnComplete(() =>
                {
                    _mySequence.Kill();
                    SetTweenPath();
                    Reset();
                })
                .SetEase(Ease.OutBounce);
                
        }
        _standState = standState;
    }

    /// <summary>
    /// 移動経路の登録をして待機する
    /// </summary>
    void SetTweenPath()
    {
        if (_wayPoint.Length > 0)
        {
            _startTween = transform.DOPath(_wayPoint, _moveDuration)
                .SetDelay(_standbyTime)
                .SetEase(Ease.Linear);
            //.OnComplete(Reset);

            //_endTween=StandSilhouette(SilhouetteStandState.Down,_rotateTime)
            //    .SetDelay(_standbyTime)
            //    .SetEase(Ease.Linear);

            _mySequence = DOTween.Sequence()
                .Append(_startTween)
                .Pause()
                .SetAutoKill(false)
                .SetLink(gameObject);
        }
    }

    void DoPlayTween()
    {
        _mySequence.Play();
    }

    void DoPauseTween()
    {
        _mySequence.Pause();
    }

    void DoKillPause()
    {
        _mySequence.Kill(false);
    }

    public void DoRestart()
    {
        _mySequence.Restart();
    }
}
