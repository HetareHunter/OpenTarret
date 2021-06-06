﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImpactTransManager : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretData;
    float deathTime;
    [SerializeField] Vector3 m_endSize;
    Vector3 m_startSize;
    Renderer renderer;
    float alpha;
    [SerializeField] float startAlpha = 0.85f;
    [SerializeField] float endAlpha = 0.0f;

    private void Awake()
    {
        deathTime = tarretData.shockWaveExistTime;
        m_startSize = transform.localScale;
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_alpha", startAlpha);
        alpha = startAlpha;
    }

    private void OnEnable()
    {
        alpha = startAlpha;
        ChangeScale();
        FadeImpact();
    }

    public void ChangeScale()
    {
        transform.DOScale(m_endSize, deathTime)
            .SetEase(Ease.Linear)
            .OnComplete(()=>InactiveImpact());
    }

    void InactiveImpact()
    {
        transform.localScale = m_startSize;
        gameObject.SetActive(false);
    }

    void FadeImpact()
    {
        DOTween.To(
            () => alpha,
            (x) => { alpha = x; renderer.material.SetFloat("_alpha", x); },
            endAlpha,
            deathTime)
            .SetEase(Ease.Linear);
    }
}
