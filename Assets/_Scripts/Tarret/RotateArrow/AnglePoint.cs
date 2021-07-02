using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

/// <summary>
/// tarretの向きを変える矢印の向きを変えるスクリプト
/// </summary>
public class AnglePoint : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject originAnglePoint;

    float adjustVerticalPosi;
    float adjustHorizontalPosi;

    Vector3 adjustPosi;

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
    // Start is called before the first frame update
    void Start()
    {
        adjustVerticalPosi = originAnglePoint.transform.localPosition.y - CenterOfHands.y;
        adjustHorizontalPosi = originAnglePoint.transform.localPosition.x - CenterOfHands.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CenterOfHands:" + CenterOfHands);
        if (BaseTarretBrain.tarretCommandState == TarretCommand.Rotate)
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
        adjustPosi = originAnglePoint.transform.localPosition - CenterOfHands;
    }
}
