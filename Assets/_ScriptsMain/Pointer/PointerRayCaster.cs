using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRayCaster : MonoBehaviour
{
    [SerializeField] GameObject line;
    [SerializeField] GameObject lineOriginPosi;
    [SerializeField] GameObject defaultLineFinishPosi;
    [SerializeField] GameObject debugHitObg;
    [SerializeField] bool isDebug = false;
    LineRenderer lineRenderer;
    Vector3[] linePosition;
    RaycastHit m_hit;
    [SerializeField] float maxRayDistance = 200.0f;
    public Vector3 hitPointerObj { private set; get; }
    //public bool isDrawLine = true;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
        linePosition = new Vector3[2];
    }


    void FixedUpdate()
    {
        RaySearchObject();
    }

    /// <summary>
    /// レイを飛ばして当たったオブジェクトが何なのかを判定する関数
    /// </summary>
    void RaySearchObject()
    {
        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(lineOriginPosi.transform.position, lineOriginPosi.transform.forward);
        if (Physics.Raycast(ray, out m_hit, maxRayDistance, LayerMask.GetMask("Enemy", "Stage", "TutorialTarget")))
        {
            hitPointerObj = m_hit.point;
            DrawLineRenderer(m_hit.point);
        }
        else
        {
            hitPointerObj = defaultLineFinishPosi.transform.position;
            DrawLineRenderer(defaultLineFinishPosi.transform.position);
        }
    }

    void DrawLineRenderer(Vector3 finishLine)
    {
        linePosition[0] = lineOriginPosi.transform.position;
        linePosition[1] = finishLine;
        lineRenderer.SetPositions(linePosition);
    }

    #region
#if UNITY_EDITOR
    private void Update()
    {
        if (isDebug)
        {
            debugHitObg.transform.position = m_hit.point;
        }
    }

#endif
    #endregion
}
