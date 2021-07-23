using System;
using System.Collections.Generic;
using UnityEngine;
using Tarret;
using UniRx;

/// <summary>
/// tarretの向きを変える矢印の向きを変えるスクリプト
/// </summary>
public class AnglePointer : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject originAnglePoint;

    Vector3 adjustPosi;
    /// <summary>
    /// センターポジションの調整処理を呼び出すディレイ時間、単位はミリ秒
    /// </summary>
    [SerializeField] float delayAdjustTime = 100.0f;
    public bool isAdjust = false;
    [SerializeField] GameObject tarret;
    TarretStateManager tarretStateManager;

    /// <summary>
    /// 両手の間の位置を返すプロパティ
    /// </summary>
    public Vector3 CenterOfHands
    {
        get
        { //両手の位置をRotateArrowオブジェクトのローカル座標系に変換している
            return transform.parent.transform.InverseTransformPoint
                (Vector3.Lerp(leftHand.transform.position, rightHand.transform.position, 0.5f));
        }
    }

    private void Start()
    {
        tarretStateManager = tarret.GetComponent<TarretStateManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("CenterOfHands:" + CenterOfHands);
        if (tarretStateManager.tarretCommandState == TarretState.Rotate && isAdjust)
        {
            MoveAnglePoint();
        }
    }

    void MoveAnglePoint()
    {
        Vector3 newPosi = CenterOfHands;

        newPosi += adjustPosi;
        newPosi.z = originAnglePoint.transform.localPosition.z;
        transform.localPosition = newPosi;
    }

    public void BeginGrabHandle()
    {
        Observable.Return(Unit.Default)
            .Delay(TimeSpan.FromMilliseconds(delayAdjustTime))
            .Subscribe(_ =>
           {
               adjustPosi = originAnglePoint.transform.localPosition - CenterOfHands;
               isAdjust = true;
           });
    }
}
