using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImpactScaleChanger : MonoBehaviour
{
    [SerializeField] TarretData tarretData;
    [SerializeField] Vector3 m_endSize;
    //[SerializeField] float m_time;

    private void Start()
    {
        ChangeScale();
    }
    public void ChangeScale()
    {
        transform.DOScale(m_endSize, tarretData.shockWaveExistTime)
            .SetEase(Ease.Linear);
    }
}
