using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImpactTransManager : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretData;
    float deathTime;
    [SerializeField] Vector3 m_endSize;
    Vector3 m_startSize;

    private void Awake()
    {
        deathTime = tarretData.shockWaveExistTime;
        m_startSize = transform.localScale;
    }

    private void OnEnable()
    {
        ChangeScale();
    }

    public void ChangeScale()
    {
        transform.DOScale(m_endSize, deathTime)
            .SetEase(Ease.Linear)
            .OnComplete(()=>FadeImpact());
    }

    void FadeImpact()
    {
        transform.localScale = m_startSize;
        gameObject.SetActive(false);
    }
}
