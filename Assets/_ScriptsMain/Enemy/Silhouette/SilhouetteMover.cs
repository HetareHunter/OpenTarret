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
    /// <summary>
    /// 現在のスタンドの状態
    /// </summary>
    SilhouetteStandState _standState = SilhouetteStandState.Down;
    // Start is called before the first frame update
    void Start()
    {
        _silhouetteActivatior = GetComponentInChildren<SilhouetteActivatior>();
    }

    // Update is called once per frame
    void Update()
    {
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
                .SetEase(Ease.OutBounce);
        }
        _standState = standState;
    }
}
