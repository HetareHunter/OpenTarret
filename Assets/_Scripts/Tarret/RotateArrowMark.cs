using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrowMark : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    /// <summary>
    /// 両手の間の位置を返すプロパティ
    /// </summary>
    public Vector3 CenterOfHands
    {
        get
        {
            return Vector3.Lerp(leftHand.transform.localPosition, rightHand.transform.localPosition, 0.5f);
        }
    }

    void RotateArrow()
    {

    }
}
