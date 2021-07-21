using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RazerEffecter : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretAttackData;
    [SerializeField] GameObject razerLineObj;
    LineRenderer razerLineRenderer;
    /// <summary>ビーム本体のエフェクト </summary>
    [SerializeField] GameObject m_razerEffect;
    /// <summary>ビームの生成位置 </summary>
    [SerializeField] GameObject m_razerEffectInsPosi;
    AttackRaycastManager attackRaycastManager;
    Vector3[] razerPosition;


    // Start is called before the first frame update
    void Start()
    {
        razerLineRenderer = razerLineObj.GetComponent<LineRenderer>();
        attackRaycastManager = GetComponent<AttackRaycastManager>();
        razerPosition = new Vector3[2];
    }

    /// <summary>
    /// レーザーのライン部分のスクリプト、存在しているものを移動して、、徐々に消えていくようにしている
    /// </summary>
    public void InstanceFireEffect()
    {
        m_razerEffect.transform.position = m_razerEffectInsPosi.transform.position;
        m_razerEffect.transform.rotation = m_razerEffectInsPosi.transform.rotation;
        m_razerEffect.SetActive(true);
        razerLineObj.SetActive(true);

        razerPosition[0] = m_razerEffectInsPosi.transform.position;
        razerPosition[1] = SetRazerFinishPosition();
        razerLineRenderer.SetPositions(razerPosition);
        razerLineRenderer.startWidth = tarretAttackData.razerWidth;
        razerLineRenderer.endWidth = tarretAttackData.razerWidth;
        DOTween.To(
            () => razerLineRenderer.startWidth,
            (x) => razerLineRenderer.startWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);
        DOTween.To(
            () => razerLineRenderer.endWidth,
            (x) => razerLineRenderer.endWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);

    }


    public void FadeFire()
    {
        m_razerEffect.SetActive(false);
        razerLineObj.SetActive(false);
    }

    Vector3 SetRazerFinishPosition()
    {
        return attackRaycastManager.FinishHitPosition();
    }
}
