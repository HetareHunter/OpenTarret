using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImpactScaleChanger : MonoBehaviour
{
    [SerializeField] Vector3 m_endSize;
    [SerializeField] float m_time;

    private void Start()
    {
        ChangeScale();
    }
    public void ChangeScale()
    {
        transform.DOScale(m_endSize, m_time)
            .SetEase(Ease.Linear);
    }
}
