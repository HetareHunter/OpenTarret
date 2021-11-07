using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Tarret;

public class DebugController : MonoBehaviour
{
    [Inject]
    ITarretState _tarretState;

    /// <summary>
    /// タレットの根本部分。ここを中心に横回転をする
    /// </summary>
    [SerializeField] GameObject rootPos;
    /// <summary>
    /// タレットの縦回転をする関節
    /// </summary>
    [SerializeField] GameObject muzzleFlameJointPos;

    [SerializeField] float maxVerticalAngle = 0.3f;
    [SerializeField] float minVerticalAngle = -0.2f;

    [Header("以下unityEditorでのデバッグ用")]
    [SerializeField] bool editRotateMode = false;
    [SerializeField] float debugHorizontalRotate = 0.8f;
    [SerializeField] float debugVerticalRotate = 0.3f;

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _tarretState.ChangeTarretState(TarretState.Attack);
        }

        if (editRotateMode)
        {
            float dx = Input.GetAxis("Horizontal");
            float dy = Input.GetAxis("Vertical");

            DebugHorizontalRotate(dx);
            DebugVerticalRotate(dy);
        }
    }

    #region
    void DebugHorizontalRotate(float dx)
    {
        rootPos.transform.Rotate(new Vector3(0, 90, 0) * dx * debugHorizontalRotate * Time.deltaTime);
    }

    void DebugVerticalRotate(float dy)
    {
        if (muzzleFlameJointPos.transform.localRotation.x > maxVerticalAngle)
        {
            if (dy < 0)
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
            }
        }
        else if (muzzleFlameJointPos.transform.localRotation.x < minVerticalAngle)
        {
            if (dy > 0)
            {
                muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
            }
        }
        else
        {
            muzzleFlameJointPos.transform.Rotate(new Vector3(90, 0, 0) * dy * debugVerticalRotate * Time.deltaTime);
        }
    }
    #endregion
#endif
}
