using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarretRemoteRotater : MonoBehaviour
{
    /// <summary>
    /// タレットの根本部分。ここを中心に横回転をする
    /// </summary>
    [SerializeField] GameObject rootPos;
    /// <summary>
    /// タレットの縦回転をする関節
    /// </summary>
    [SerializeField] GameObject muzzleFlameJointPos;

    /// <summary>
    /// タレットの向きを決定する矢印
    /// </summary>
    [SerializeField] GameObject m_arrowMark;

    [SerializeField] float maxVerticalAngle = 0.3f;
    [SerializeField] float minVerticalAngle = -0.2f;

    [SerializeField] float maxHorizontalAngle = 0.5f;
    [SerializeField] float minHorizontalAngle = -0.5f;

    [SerializeField] float rotateSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// 横回転を制御する関数
    /// </summary>
    void HorizontalRotate()
    {
        if (rootPos.transform.localRotation.y > maxHorizontalAngle)
        {
            if (m_arrowMark.transform.localRotation.y < 0)
            {
                rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.y);
            }
        }
        else if (rootPos.transform.localRotation.y < minHorizontalAngle)
        {
            if (m_arrowMark.transform.localRotation.y > 0)
            {
                rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.y);
            }
        }
        else
        {
            rootPos.transform.Rotate(new Vector3(0, 90, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.y);
        }
    }

    /// <summary>
    /// 縦回転を制御する関数
    /// </summary>
    void VerticalRotate()
    {
        if (muzzleFlameJointPos.transform.localRotation.x > maxVerticalAngle)
        {
            if (m_arrowMark.transform.localRotation.x < 0)
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.x);
            }
        }
        else if (muzzleFlameJointPos.transform.localRotation.x < minVerticalAngle)
        {
            if (m_arrowMark.transform.localRotation.x > 0)
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.x);
            }
        }
        else
        {
            muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * rotateSpeed * Time.deltaTime
                    * m_arrowMark.transform.localRotation.x);
        }
    }
}
