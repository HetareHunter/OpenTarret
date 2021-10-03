using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ターゲットであるシルエットを起こしたり倒したりするクラス。DoTweenでアニメーションさせている
/// </summary>
public class SilhouetteMover : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1.0f;
    bool _isStand = false;
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
            _isStand = true;
            RotatePivot();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _isStand = false;
            RotatePivot();
        }
    }

    public void RotatePivot()
    {
        if (_isStand)
        {
            transform.DORotate(new Vector3(-90, 0, 0), rotateSpeed, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        else
        {
            transform.DORotate(new Vector3(90, 0, 0), rotateSpeed, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
    }
}
