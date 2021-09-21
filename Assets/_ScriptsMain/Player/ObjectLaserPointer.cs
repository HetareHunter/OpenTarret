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
        public Hand _hand;
        [SerializeField] GameObject defaultLineFinishPosi;
        [SerializeField] GameObject customHand;
        LineRenderer lineRenderer;
        /// <summary>
        /// 手から出るレーザー。レイを可視化しているだけのものなので始点と終点の2点の座標を格納する
        /// </summary>
        Vector3[] linePosition = new Vector3[2];
        RaycastHit _hit;
        [SerializeField] float maxRayDistance = 0.5f;
        ISelectable selectable;
        IGrabbable grabbable;

        [SerializeField] float grabBegin = 0.55f;
        [SerializeField] float grabEnd = 0.35f;
        bool _preGrab = false;

        /// <summary>
        /// レイで掴めるものを探索できるかどうかのフラグ。
        /// 何かを掴んでいる間は探索できないようにする
        /// </summary>
        bool _searchable = true;


        // Start is called before the first frame update
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RaySearchObject();
            CheckForGrabOrRelease();
        }

        /// <summary>
        /// レイを飛ばして当たったオブジェクトが何なのかを判定する関数
        /// </summary>
        void RaySearchObject()
        {
            if (!_searchable)
            {
                DrawLineRenderer(customHand.transform.position);
                return;
            }
            //　飛ばす位置と飛ばす方向を設定
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out _hit, maxRayDistance, LayerMask.GetMask("Tarret")))
            {
                DrawLineRenderer(_hit.point);
                if (selectable == null)
                {
                    selectable = _hit.transform.GetComponent<ISelectable>();
                }
                if (grabbable == null)
                {
                    grabbable = _hit.transform.GetComponent<IGrabbable>();
                }

                selectable.OnTouch(true, _hand);
                
            }
            else
            {
                if (selectable != null)
                {
                    selectable.OnTouch(false, _hand);
                    selectable = null;
                }
                if (grabbable != null)
                {
                    grabbable = null;
                }
                DrawLineRenderer(defaultLineFinishPosi.transform.position);
            }
        }

        void DrawLineRenderer(Vector3 finishLine)
        {
            linePosition[0] = customHand.transform.position;
            linePosition[1] = finishLine;
            lineRenderer.SetPositions(linePosition);
        }

        void CheckForGrabOrRelease()
        {
            if (grabbable == null) return;

            if (_hand == Hand.Left)
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) >= grabBegin)
                {
                    _preGrab = true;
                    _searchable = false;
                    grabbable.GrabBegin(OVRInput.Controller.LTouch, transform);
                }
                else if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) <= grabEnd && _preGrab)
                {
                    _preGrab = false;
                    grabbable.GrabEnd();
                    grabbable = null;
                    _searchable = true;
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) >= grabBegin)
                {
                    _preGrab = true;
                    _searchable = false;
                    grabbable.GrabBegin(OVRInput.Controller.RTouch, transform);
                }
                else if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) <= grabEnd && _preGrab)
                {
                    _preGrab = false;
                    grabbable.GrabEnd();
                    grabbable = null;
                    _searchable = true;
                }
            }
        }
    }
}