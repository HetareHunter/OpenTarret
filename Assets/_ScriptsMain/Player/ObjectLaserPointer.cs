using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hand
{
    Left,
    Right,
    None
}

namespace Players
{
    public interface ISelectable
    {
        public Hand SelectHand { get; set; }
        public void SelectHandle(bool isTouch, Hand hand);
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
        /// �肩��o�郌�[�U�[�B���C���������Ă��邾���̂��̂Ȃ̂Ŏn�_�ƏI�_��2�_�̍��W���i�[����
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
        /// ���C�Œ͂߂���̂�T���ł��邩�ǂ����̃t���O�B
        /// ������͂�ł���Ԃ͒T���ł��Ȃ��悤�ɂ���
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
        /// ���C���΂��ē��������I�u�W�F�N�g�����Ȃ̂��𔻒肷��֐�
        /// </summary>
        void RaySearchObject()
        {
            if (!_searchable)
            {
                DrawLineRenderer(customHand.transform.position);
                return;
            }
            //�@��΂��ʒu�Ɣ�΂�������ݒ�
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out _hit, maxRayDistance, LayerMask.GetMask("Tarret")))
            {
                DrawLineRenderer(_hit.point);
                if (selectable == null)
                {
                    selectable = _hit.transform.GetComponent<ISelectable>();
                }

                if (selectable != null)
                {
                    if (selectable.SelectHand == _hand || selectable.SelectHand == Hand.None)//�I�����Ă���肪���̃R���|�[�l���g�̎�A���邢�͑I�����Ă���肪�Ȃ��ꍇ
                    {
                        selectable.SelectHandle(true, _hand);
                        if (grabbable == null)//�͂ނ��Ƃ��ł���悤�ɂ���
                        {
                            grabbable = _hit.transform.GetComponent<IGrabbable>();
                        }
                    }
                    else //�I�����Ă���肪���̃R���|�[�l���g�ł͂Ȃ���ł���ꍇ
                    {
                        selectable = null;
                    }
                    
                }

            }
            else
            {
                if (selectable != null)//���C���O���u�ԂɌĂяo����鏈��
                {
                    selectable.SelectHandle(false, Hand.None);
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
            else if (_hand == Hand.Right)
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