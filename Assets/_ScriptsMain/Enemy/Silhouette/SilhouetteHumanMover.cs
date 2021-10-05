using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ターゲットであるシルエットを起こしたり倒したりするクラス。DoTweenでアニメーションさせている
/// 人のシルエットはPositionが動かない
/// </summary>
public class SilhouetteHumanMover : MonoBehaviour, IMovableSilhouette
{
    [SerializeField] float _rotateTime = 1.0f;
    SilhouetteHumanDeath _silhouetteHumanDeath;
    /// <summary>
    /// 現在のスタンドの状態
    /// </summary>
    SilhouetteStandState _standState = SilhouetteStandState.Down;
    // Start is called before the first frame update
    void Start()
    {
        _silhouetteHumanDeath = GetComponentInChildren<SilhouetteHumanDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StandSilhouette(SilhouetteStandState.Up, _rotateTime);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StandSilhouette(SilhouetteStandState.Down, _rotateTime / 2);
        }
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
