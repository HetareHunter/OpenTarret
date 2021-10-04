using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ターゲットであるシルエットを起こしたり倒したりするクラス。DoTweenでアニメーションさせている
/// </summary>
public class SilhouetteMover : MonoBehaviour
{
    public enum Stand
    {
        Up,
        Down
    }

    [SerializeField] float _rotateTime = 1.0f;
    /// <summary>
    /// 現在のスタンドの状態
    /// </summary>
    Stand _standState = Stand.Down;
    // Start is called before the first frame update
    void Start()
    {
        //RotatePivot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            RotatePivot(Stand.Up, _rotateTime);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            RotatePivot(Stand.Down, _rotateTime / 2);
        }
    }

    /// <summary>
    /// シルエットの回転軸を回転させるメソッド
    /// </summary>
    /// <param name="standState">これからこの状態にしたいというhikisuu
    /// </param>
    /// <param name="rotateTime"></param>
    public void RotatePivot(Stand standState, float rotateTime)
    {
        if (_standState == Stand.Down && standState == Stand.Up)
        {
            transform.DORotate(new Vector3(-90, 0, 0), rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        else if (_standState == Stand.Up && standState == Stand.Down)
        {
            transform.DORotate(new Vector3(90, 0, 0), rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        _standState = standState;
    }
}
