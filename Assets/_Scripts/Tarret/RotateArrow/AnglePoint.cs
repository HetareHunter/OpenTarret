using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class AnglePoint : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject originAnglePoint;

    [SerializeField] float limitMoveDistance = 5.0f;
    [SerializeField, Range(0, 1.0f)] float limitLeapValue = 0.05f;

    float adjustHeightPosi;


    /// <summary>
    /// 両手の間の位置を返すプロパティ
    /// </summary>
    public Vector3 CenterOfHands
    {
        get
        {
            return transform.parent.transform.InverseTransformPoint
                (Vector3.Lerp(leftHand.transform.position, rightHand.transform.position, 0.5f));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        adjustHeightPosi = originAnglePoint.transform.localPosition.y - CenterOfHands.y;
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
        newPosi.z = originAnglePoint.transform.localPosition.z;
        newPosi.y += adjustHeightPosi;
        transform.localPosition = newPosi;
        if (!(Vector3.Distance(newPosi, originAnglePoint.transform.position) > limitMoveDistance))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosi, 1.0f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosi, limitLeapValue);
        }
    }


    void BeginGrabHandle()
    {

    }
}
