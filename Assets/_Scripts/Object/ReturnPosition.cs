using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

namespace Players
{
    public class ReturnPosition : MonoBehaviour
    {
        Rigidbody m_rb;
        [SerializeField] Transform returningPosition;
        Vector3 startRotation;
        [SerializeField] float returnSpeed = 1.0f;

        // Start is called before the first frame update
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
            startRotation = transform.localEulerAngles;
        }

        public void Released()
        {
            transform.DOLocalMove(returningPosition.position, returnSpeed)
                .SetEase(Ease.OutCirc)
                .OnComplete(() => m_rb.velocity = Vector3.zero);
            transform.DOLocalRotate(startRotation, returnSpeed);

        }

    }
}