using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hand
{
    Left,
    Right
}

namespace Players
{
    public interface ISelectable
    {
        public void OnTouch(bool isTouch, Hand hand);
        public void ChangeOutlineColor(bool isTouch);
        public void VanishOutline();
    }


    public class ObjectLaserPointer : MonoBehaviour
    {
        public Hand hand;
        [SerializeField] GameObject defaultLineFinishPosi;
        //[SerializeField] GameObject HitPointer;
        [SerializeField] GameObject customHand;
        LineRenderer lineRenderer;
        Vector3[] linePosition;
        RaycastHit m_hit;
        [SerializeField] float maxRayDistance = 0.5f;
        ISelectable selectable;


        public Vector3 hitPointerObj { private set; get; }


        // Start is called before the first frame update
        void Start()
        {
            linePosition = new Vector3[2];
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
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
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out m_hit, maxRayDistance, LayerMask.GetMask("Tarret")))
            {
                hitPointerObj = m_hit.point;
                DrawLineRenderer(m_hit.point);
                //SetHitObjPosition();
                if (selectable == null)
                {
                    selectable = m_hit.transform.GetComponent<ISelectable>();
                }
                selectable.OnTouch(true,hand);
                
            }
            else
            {
                if (selectable != null)
                {
                    selectable.OnTouch(false, hand);
                    selectable = null;
                }
                hitPointerObj = defaultLineFinishPosi.transform.position;
                DrawLineRenderer(defaultLineFinishPosi.transform.position);
            }
        }

        void DrawLineRenderer(Vector3 finishLine)
        {
            linePosition[0] = customHand.transform.position;
            linePosition[1] = finishLine;
            lineRenderer.SetPositions(linePosition);
        }

        //void SetHitObjPosition()
        //{
        //    HitPointer.transform.position = m_hit.point;
        //}
    }
}