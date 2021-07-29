using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerRayCaster : MonoBehaviour
{
    [SerializeField] GameObject line;
    [SerializeField] GameObject lineOriginPosi;
    [SerializeField] GameObject defaultLineFinishPosi;
    LineRenderer lineRenderer;
    Vector3[] linePosition;
    RaycastHit m_hit;
    [SerializeField] float maxRayDistance = 200.0f;
    public Vector3 hitPointerObj { get; private set; }
    public bool isDrawLine = true;
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
    /// ���C���΂��ē��������I�u�W�F�N�g�����Ȃ̂��𔻒肷��֐�
    /// </summary>
    void RaySearchObject()
    {
        //�@��΂��ʒu�Ɣ�΂�������ݒ�
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

    //public void OnPointer (bool draw)
    //{
    //    isDrawLine = draw;
    //}

}
